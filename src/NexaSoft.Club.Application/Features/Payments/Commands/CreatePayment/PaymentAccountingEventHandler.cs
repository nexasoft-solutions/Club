using MediatR;
using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Features.Payments.Events;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;

public class PaymentAccountingEventHandler(
    IGenericRepository<AccountingEntry> _accountingEntryRepository,
    IGenericRepository<AccountingEntryItem> _entryItemRepository,
    IGenericRepository<Payment> _paymentRepository,
    IGenericRepository<Member> _memberRepository,
    IGenericRepository<MemberFee> _memberFeeRepository,
    IGenericRepository<PaymentItem> _paymentItemRepository,
    IGenericRepository<FeeConfiguration> _feeConfigurationRepository,
    IGenericRepository<AccountingChart> _accountingChartRepository,
    IGenericRepository<Contador> _contadorRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork,
    ILogger<PaymentAccountingEventHandler> _logger
) : INotificationHandler<PaymentItemsCreatedDomainEvent>
{
    public async Task Handle(PaymentItemsCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generando asiento contable para pago ID: {PaymentId}", notification.PaymentId);

        try
        {

            var payment = await _paymentRepository.GetByIdAsync(notification.PaymentId, cancellationToken);
            var member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);

            if (payment == null || member == null)
            {
                _logger.LogError("Payment o Member no encontrados para generar asiento contable");
                return;
            }

            // 1. GENERAR ASIENTO CONTABLE
            var accountingEntry = await GenerateAccountingEntry(payment, member, notification, cancellationToken);

            // 2. GENERAR ITEMS DEL ASIENTO
            await GenerateAccountingEntryItems(accountingEntry, payment, member, notification, cancellationToken);

            // 3. ACTUALIZAR PAYMENT CON EL ACCOUNTING ENTRY ID
            payment.SetAccountingEntryId(accountingEntry.Id);
            await _paymentRepository.UpdateAsync(payment);

            // 4. DISPARAR EVENTO DE ASIENTO CONTABLE GENERADO
            payment.RaiseDomainEvent(new PaymentAccountingEntryGeneratedDomainEvent(
                Guid.NewGuid(),
                payment.Id,
                accountingEntry.Id,
                notification.CreatedBy,
                _dateTimeProvider.CurrentTime.ToUniversalTime()
            ));


            _logger.LogInformation("Asiento contable generado exitosamente para pago ID: {PaymentId}", notification.PaymentId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar asiento contable para pago ID: {PaymentId}", notification.PaymentId);
            throw;
        }
    }

    private async Task<AccountingEntry> GenerateAccountingEntry(
        Payment payment,
        Member member,
        PaymentItemsCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var entryNumber = await GenerateUniqueEntryNumber(payment.CreatedBy!,cancellationToken);//Guid.NewGuid().ToString();//await GenerateUniqueEntryNumber();

        var accountingEntry = AccountingEntry.Create(
            entryNumber,
            payment.PaymentDate,
            $"Pago recibido - {member.FirstName} {member.LastName} - {payment.ReceiptNumber}",
            "Pagos",
            payment.Id,
            payment.TotalAmount,
            payment.TotalAmount,
            false,
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            notification.CreatedBy
        );

        await _accountingEntryRepository.AddAsync(accountingEntry, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return accountingEntry;
    }

    private async Task GenerateAccountingEntryItems(
        AccountingEntry accountingEntry,
        Payment payment,
        Member member,
        PaymentItemsCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        // 1. DÉBITO: Caja/Bancos
        var debitAccountId = await GetCashAccountId(notification.PaymentMethod, cancellationToken);

        var debitItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            debitAccountId,
            payment.TotalAmount,
            0.00M,
            $"Ingreso por pago - {notification.PaymentMethod} - Comprobante: {payment.ReceiptNumber}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            notification.CreatedBy
        );

        await _entryItemRepository.AddAsync(debitItem, cancellationToken);

        var paymentItems = await LoadPaymentItemsForPayment(payment.Id, cancellationToken);

        //Console.WriteLine($"Número de PaymentItems encontrados: {paymentItems.Count}");


        var creditAccountIds = new HashSet<long>(); // Usamos HashSet para asegurar unicidad
        // 🔹 2. CRÉDITOS: uno por cada PaymentItem según su FeeConfiguration
        foreach (var item in paymentItems)
        {

            var specParams = new BaseSpecParams { Id = item.MemberFeeId };
            var spec = new MemberFeeSpecification(specParams);
            var memberFee = await _memberFeeRepository.GetEntityWithSpec(spec, cancellationToken);

            long creditAccountId;

            //Console.WriteLine($"item => {item.MemberFeeId}");

            //Console.WriteLine($"FeeConfigurationId => {memberFee?.FeeConfigurationId}");

            if (memberFee?.FeeConfigurationId != null)
            {
                // Buscar FeeConfiguration para obtener la cuenta de ingreso
                var feeConfig = await _feeConfigurationRepository.GetByIdAsync(memberFee.FeeConfigurationId, cancellationToken);

                creditAccountId = feeConfig?.IncomeAccountId
                                      ?? await GetAccountIdByCode("4.1.1.1", cancellationToken);

                var description = $"{feeConfig?.FeeName ?? "General"} - {member.FirstName} {member.LastName}";

                var creditItem = AccountingEntryItem.Create(
                    accountingEntry.Id,
                    creditAccountId,
                    0.00M,                 // DÉBITO
                    item.Amount,           // CRÉDITO
                    description,
                    (int)EstadosEnum.Activo,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    notification.CreatedBy
                );

                await _entryItemRepository.AddAsync(creditItem, cancellationToken);
            }
            else
            {
                // fallback si no hay FeeConfiguration → usar cuenta default
                creditAccountId = await GetAccountIdByCode("4.1.1.1", cancellationToken);

                var description = $"Pago general - {member.FirstName} {member.LastName}";

                var creditItem = AccountingEntryItem.Create(
                    accountingEntry.Id,
                    creditAccountId,
                    0.00M,
                    item.Amount,
                    description,
                    (int)EstadosEnum.Activo,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                     notification.CreatedBy
                );

                await _entryItemRepository.AddAsync(creditItem, cancellationToken);
            }
            creditAccountIds.Add(creditAccountId); // HashSet evita duplicados automáticamente
        }



        _logger.LogInformation("Items contables generados: Débito en cuenta {DebitAccount}, Crédito en cuenta {CreditAccount}",
            debitAccountId, string.Join(", ", creditAccountIds));
    }

    // 🔹 MÉTODO PARA CARGAR PAYMENT ITEMS
    private async Task<List<PaymentItemsResponse>> LoadPaymentItemsForPayment(long paymentId, CancellationToken cancellationToken)
    {
        try
        {
            var specParams = new BaseSpecParams
            {
                NoPaging = true,
                SearchFields = "payment",
                Search = paymentId.ToString()
            };

            var spec = new PaymentItemsByPaymentSpec(specParams);
            var items = await _paymentItemRepository.ListAsync(spec, cancellationToken);

            var result = items.Select(x => new PaymentItemsResponse(
                x.Id,
                x.PaymentId,
                x.MemberFeeId,
                x.Amount
            )).ToList();

            Console.WriteLine($"PaymentItems cargados desde BD: {items.Count} para PaymentId: {paymentId}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error cargando PaymentItems: {ex.Message}");
            _logger.LogError(ex, "Error al cargar PaymentItems para payment {PaymentId}", paymentId);
            return new List<PaymentItemsResponse>();
        }
    }


    private async Task<long> GetAccountIdByCode(string accountCode, CancellationToken cancellationToken)
    {
        try
        {
            // Buscar la cuenta contable por código
            var account = await _accountingChartRepository.ListAsync(cancellationToken);
            var specificAccount = account.FirstOrDefault(a => a.AccountCode == accountCode);

            if (specificAccount == null)
            {
                _logger.LogWarning("No se encontró la cuenta contable con código {AccountCode}, usando cuenta por defecto", accountCode);
                // Buscar una cuenta de activo por defecto
                var defaultAccount = account.FirstOrDefault(a => a.AccountCode == "1.1.1.1");
                return defaultAccount?.Id ?? 1; // Fallback hardcodeado
            }

            return specificAccount.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cuenta contable con código {AccountCode}", accountCode);
            return 1; // Fallback hardcodeado
        }
    }


    private async Task<string> GenerateUniqueEntryNumber(string createdBy, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.Today;
            var formattedDate = today.ToString("yyyyMMdd");
            //var count = 1;//todayEntries.Count;
            //return $"AE-{today:yyyyMMdd}-{count + 1:0000}";
            var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("Pago"), cancellationToken);


            if (contador == null)
            {
                // Si no existe, creamos uno inicial
                var contadorNew = Contador.Create(
                    "Pago",
                    "AE",
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

            // Incrementar valor actual
            var nuevoCodigo = contador.Incrementar(_dateTimeProvider.CurrentTime.ToUniversalTime(), createdBy, formattedDate);
            return nuevoCodigo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al generar número de asiento único");
            return $"AE-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }


    private async Task<long> GetCashAccountId(string paymentMethod, CancellationToken cancellationToken)
    {
        // Determinar cuenta contable según método de pago
        var accountCode = paymentMethod?.ToLower() switch
        {
            "cash" or "efectivo" => "1.1.1.1", // Caja General
            "creditcard" or "debitcard" or "tarjeta" => "1.1.1.2", // Bancos
            "transfer" or "transferencia" => "1.1.1.2", // Bancos
            _ => "1.1.1.1" // Por defecto Caja General
        };

        return await GetAccountIdByCode(accountCode, cancellationToken);
    }

}

