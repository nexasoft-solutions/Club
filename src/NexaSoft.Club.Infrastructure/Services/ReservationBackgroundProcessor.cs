using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Reservations.Commands.CreateReservation;
using NexaSoft.Club.Application.Features.Reservations.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Features.Reservations;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Masters.SpaceRates;
using NexaSoft.Club.Domain.Masters.Spaces;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Infrastructure.Services;

public class ReservationBackgroundProcessor : IReservationBackgroundProcessor
{
    private readonly IGenericRepository<Reservation> _reservationRepository;
    private readonly IGenericRepository<Payment> _paymentRepository;
    private readonly IGenericRepository<AccountingEntry> _accountingEntryRepository;
    private readonly IGenericRepository<AccountingEntryItem> _entryItemRepository;
    private readonly IGenericRepository<SpaceRate> _spaceRateRepository;
    private readonly IGenericRepository<AccountingChart> _accountingChartRepository;
    private readonly IGenericRepository<Contador> _contadorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<ReservationBackgroundProcessor> _logger;

    public ReservationBackgroundProcessor(
        IGenericRepository<Reservation> reservationRepository,
        IGenericRepository<Payment> paymentRepository,
        IGenericRepository<AccountingEntry> accountingEntryRepository,
        IGenericRepository<AccountingEntryItem> entryItemRepository,
        IGenericRepository<SpaceRate> spaceRateRepository,
        IGenericRepository<AccountingChart> accountingChartRepository,
        IGenericRepository<Contador> contadorRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        ILogger<ReservationBackgroundProcessor> logger)
    {
        _reservationRepository = reservationRepository;
        _paymentRepository = paymentRepository;
        _accountingEntryRepository = accountingEntryRepository;
        _entryItemRepository = entryItemRepository;
        _spaceRateRepository = spaceRateRepository;
        _accountingChartRepository = accountingChartRepository;
        _contadorRepository = contadorRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task ProcessReservationAsync(long reservationId, CreateReservationCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Procesando reserva {ReservationId} en background", reservationId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. CARGAR DATOS
            var reservation = await _reservationRepository.GetByIdAsync(reservationId, cancellationToken);
            if (reservation == null)
            {
                throw new InvalidOperationException($"Reserva {reservationId} no encontrada");
            }

            // 2. CREAR PAGO ASOCIADO A LA RESERVA
            //var payment = await CreatePaymentForReservation(reservation, command, cancellationToken);

            // 3. GENERAR ASIENTO CONTABLE
            var accountingEntry = await GenerateAccountingEntryForReservation(reservation, command, cancellationToken);

            // 4. ACTUALIZAR RESERVA CON LOS IDs GENERADOS
            reservation.SetAccountingEntryId(accountingEntry.Id);
            reservation.MarkAsCompleted(accountingEntry.Id);
            await _reservationRepository.UpdateAsync(reservation);

            // 5. GUARDAR TODO
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Reserva {ReservationId} procesada exitosamente en background", reservationId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error en procesamiento background para reserva {ReservationId}", reservationId);
            throw;
        }
    }

    
    /*private async Task<Payment> CreatePaymentForReservation(Reservation reservation, CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var payment = Payment.Create(
            command.MemberId,
            reservation.TotalAmount,
            reservation.Date, //.ToDateTime(TimeOnly.MinValue), // Convertir DateOnly a DateTime
            command.PaymentMethodId, //?? (long)PaymentMethodEnum.Efectivo, // Default a efectivo
            command.ReferenceNumber,
            command.DocumentTypeId,
            reservation.ReceiptNumber, // Usar número de reserva como referencia
            isPartial: false,
            accountingEntryId: null, // Se asignará después
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy,
            statusId: (long)StatusEnum.Completado
        //sourceModuleId: (long)SourceModuleEnum.Reservaciones,
        //sourceId: reservation.Id
        );

        await _paymentRepository.AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Guardar para obtener ID

        _logger.LogInformation("Pago {PaymentId} creado para reserva {ReservationId}", payment.Id, reservation.Id);

        return payment;
    }*/

    private async Task<AccountingEntry> GenerateAccountingEntryForReservation(
        Reservation reservation,
        CreateReservationCommand command,
        CancellationToken cancellationToken)
    {
        var entryNumber = await GenerateUniqueEntryNumber(command.CreatedBy!, cancellationToken);

        // Cargar SpaceRate con Space y MemberType
        var spaceRateSpec = new SpaceRateWithSpaceAndMemberTypeSpec(reservation.SpaceRateId);
        var spaceRate = await _spaceRateRepository.GetEntityWithSpec(spaceRateSpec, cancellationToken);

        var spaceName = spaceRate?.Space?.SpaceName ?? "Espacio";
        //var memberTypeName = spaceRate?.MemberType?.TypeName ?? "Miembro";

        var accountingEntry = AccountingEntry.Create(
            entryNumber,
            reservation.Date, 
            $"Ingreso por reserva - {spaceName} - {reservation.ReceiptNumber}",
            (long)SourceModuleEnum.Reservaciones,
            reservation.Id,
            reservation.TotalAmount,
            reservation.TotalAmount,
            false,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _accountingEntryRepository.AddAsync(accountingEntry, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Guardar para obtener ID

        // Generar items del asiento contable
        await GenerateAccountingEntryItemsForReservation(accountingEntry, reservation, spaceRate?.Space, command, cancellationToken);

        return accountingEntry;
    }

    private async Task GenerateAccountingEntryItemsForReservation(
        AccountingEntry accountingEntry,
        Reservation reservation,
        Space? space,
        CreateReservationCommand command,
        CancellationToken cancellationToken)
    {
        // 1. DÉBITO: Caja/Bancos
        var debitAccountId = await GetCashAccountId(command.PaymentMethodId, cancellationToken);

        var debitItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            debitAccountId,
            reservation.TotalAmount,
            0.00M,
            $"Ingreso por reserva - Comprobante: {reservation.ReceiptNumber}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _entryItemRepository.AddAsync(debitItem, cancellationToken);

        // 2. CRÉDITO: Ingresos por alquiler de espacios
        var creditAccountId = space?.IncomeAccountId ?? await GetAccountIdByCode("4.1.2.1", cancellationToken);

        var creditItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            creditAccountId,
            0.00M,
            reservation.TotalAmount,
            $"Ingreso por alquiler - {space?.SpaceName ?? "Espacio"} - {reservation.Date:dd/MM/yyyy}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _entryItemRepository.AddAsync(creditItem, cancellationToken);

        _logger.LogInformation("Items contables generados para reserva {ReservationId}: Débito {DebitAccount}, Crédito {CreditAccount}",
            reservation.Id, debitAccountId, creditAccountId);
    }

    private async Task<string> GenerateUniqueEntryNumber(string createdBy, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.Today;
            var formattedDate = today.ToString("yyyyMMdd");

            var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("Reserva"), cancellationToken);

            if (contador == null)
            {
                var contadorNew = Contador.Create(
                    "Reserva",
                    "AR",
                    1,
                    string.Empty,
                    "string",
                    10,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    createdBy
                );

                await _contadorRepository.AddAsync(contadorNew, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                contador = contadorNew;
            }

            var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(), createdBy, formattedDate);
            return nuevoCodigo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar número de asiento único para reserva");
            return $"AR-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }

   


    private async Task<long> GetCashAccountId(long paymentMethodId, CancellationToken cancellationToken)
    {
        var accountCode = paymentMethodId switch
        {
            (long)PaymentMethodEnum.Efectivo => "1.1.1.1",
            (long)PaymentMethodEnum.TarjetaDeCredito => "1.1.1.2",
            (long)PaymentMethodEnum.TarjetaDeDebito => "1.1.1.2",
            (long)PaymentMethodEnum.TransferenciaBancaria => "1.1.1.2",
            _ => "1.1.1.1"
        };

        return await GetAccountIdByCode(accountCode, cancellationToken);
    }

    private async Task<long> GetAccountIdByCode(string accountCode, CancellationToken cancellationToken)
    {
        try
        {
            var accounts = await _accountingChartRepository.ListAsync(cancellationToken);
            var account = accounts.FirstOrDefault(a => a.AccountCode == accountCode);

            if (account == null)
            {
                _logger.LogWarning("No se encontró la cuenta contable con código {AccountCode}, usando cuenta por defecto", accountCode);
                var defaultAccount = accounts.FirstOrDefault(a => a.AccountCode == "1.1.1.1");
                return defaultAccount?.Id ?? 1;
            }

            return account.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cuenta contable {AccountCode}", accountCode);
            return 1;
        }
    }
}