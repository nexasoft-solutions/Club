using Microsoft.Extensions.Logging;
using NexaSoft.Club.Application.Abstractions.Time;
using NexaSoft.Club.Application.Features.Payments.Commands.CreatePayment;
using NexaSoft.Club.Application.Features.Payments.Services;
using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Features.MemberFees;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.Payments;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Masters.Contadores;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;
using NexaSoft.Club.Domain.Specifications;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Infrastructure.Services;

public class PaymentBackgroundProcessor : IPaymentBackgroundProcessor
{
    private readonly IGenericRepository<Payment> _paymentRepository;
    private readonly IGenericRepository<PaymentItem> _paymentItemRepository;
    private readonly IGenericRepository<MemberFee> _memberFeeRepository;
    private readonly IGenericRepository<Member> _memberRepository;
    private readonly IGenericRepository<AccountingEntry> _accountingEntryRepository;
    private readonly IGenericRepository<AccountingEntryItem> _entryItemRepository;
    private readonly IGenericRepository<FeeConfiguration> _feeConfigurationRepository;
    private readonly IGenericRepository<AccountingChart> _accountingChartRepository;
    private readonly IGenericRepository<Contador> _contadorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<PaymentBackgroundProcessor> _logger;

    public PaymentBackgroundProcessor(
        IGenericRepository<Payment> paymentRepository,
        IGenericRepository<PaymentItem> paymentItemRepository,
        IGenericRepository<MemberFee> memberFeeRepository,
        IGenericRepository<Member> memberRepository,
        IGenericRepository<AccountingEntry> accountingEntryRepository,
        IGenericRepository<AccountingEntryItem> entryItemRepository,
        IGenericRepository<FeeConfiguration> feeConfigurationRepository,
        IGenericRepository<AccountingChart> accountingChartRepository,
        IGenericRepository<Contador> contadorRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        ILogger<PaymentBackgroundProcessor> logger)
    {
        _paymentRepository = paymentRepository;
        _paymentItemRepository = paymentItemRepository;
        _memberFeeRepository = memberFeeRepository;
        _memberRepository = memberRepository;
        _accountingEntryRepository = accountingEntryRepository;
        _entryItemRepository = entryItemRepository;
        _feeConfigurationRepository = feeConfigurationRepository;
        _accountingChartRepository = accountingChartRepository;
        _contadorRepository = contadorRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    public async Task ProcessPaymentAsync(long paymentId, CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Procesando pago {PaymentId} en background", paymentId);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 1. CARGAR DATOS
            var payment = await _paymentRepository.GetByIdAsync(paymentId, cancellationToken);
            var member = await _memberRepository.GetByIdAsync(command.MemberId, cancellationToken);

            if (payment == null || member == null)
            {
                throw new InvalidOperationException($"Payment {paymentId} o Member {command.MemberId} no encontrados");
            }

            // 2. PROCESAR ITEMS DE PAGO
            var paymentItems = await ProcessPaymentItems(command, payment, cancellationToken);

            // 3. GENERAR CONTABILIDAD (ASIENTO + ITEMS)
            var accountingEntry = await GenerateAccountingEntry(payment, member, command, cancellationToken);

            // 4. ACTUALIZAR PAYMENT CON EL ACCOUNTING ENTRY ID
            payment.SetAccountingEntryId(accountingEntry.Id);
            await _paymentRepository.UpdateAsync(payment);

            // ACTUALIZAR PAYMENT COMO COMPLETADO
            payment.MarkAsCompleted(accountingEntry.Id);
            await _paymentRepository.UpdateAsync(payment);

            // 5. GUARDAR TODO
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation("Pago {PaymentId} procesado exitosamente en background", paymentId);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error en procesamiento background para pago {PaymentId}", paymentId);
            throw;
        }
    }

    private async Task<List<PaymentItem>> ProcessPaymentItems(
        CreatePaymentCommand command,
        Payment payment,
        CancellationToken cancellationToken)
    {
        var paymentItems = new List<PaymentItem>();

        if (command.PaymentItems != null && command.PaymentItems.Any())
        {
            // Pago específico por items
            foreach (var item in command.PaymentItems)
            {
                var memberFee = await _memberFeeRepository.GetByIdAsync(item.MemberFeeId, cancellationToken);
                if (memberFee == null) continue;

                var paymentItem = PaymentItem.Create(
                    payment.Id,
                    memberFee.Id,
                    item.AmountToPay,
                    (int)EstadosEnum.Activo,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    command.CreatedBy
                );

                await _paymentItemRepository.AddAsync(paymentItem, cancellationToken);

                memberFee.ApplyPayment(item.AmountToPay, _dateTimeProvider.CurrentTime.ToUniversalTime(), command.CreatedBy);
                await _memberFeeRepository.UpdateAsync(memberFee);

                paymentItems.Add(paymentItem);
            }
        }
        else
        {
            // Pago general automático
            paymentItems = await ProcessGeneralPayment(command, payment, cancellationToken);
        }

        return paymentItems;
    }

    private async Task<List<PaymentItem>> ProcessGeneralPayment(
        CreatePaymentCommand command,
        Payment payment,
        CancellationToken cancellationToken)
    {
        var paymentItems = new List<PaymentItem>();
        var pendingFees = await _memberFeeRepository.ListAsync(
            new PendingFeesByMemberWithFeeConfigSpec(command.MemberId),
            cancellationToken
        );

        decimal remaining = command.Amount;

        foreach (var fee in pendingFees
            .OrderBy(f => f.MemberTypeFee!.FeeConfiguration.Priority)
            .ThenBy(f => f.DueDate))
        {
            if (remaining <= 0) break;

            var toPay = Math.Min(remaining, fee.RemainingAmount);

            var paymentItem = PaymentItem.Create(
                payment.Id,
                fee.Id,
                toPay,
                (int)EstadosEnum.Activo,
                _dateTimeProvider.CurrentTime.ToUniversalTime(),
                command.CreatedBy
            );

            await _paymentItemRepository.AddAsync(paymentItem, cancellationToken);

            fee.ApplyPayment(toPay, _dateTimeProvider.CurrentTime.ToUniversalTime(), command.CreatedBy);
            await _memberFeeRepository.UpdateAsync(fee);

            paymentItems.Add(paymentItem);
            remaining -= toPay;
        }

        // Si sobra saldo, agregar como crédito
        if (remaining > 0)
        {
            payment.AddCreditBalance(remaining);
            await _paymentRepository.UpdateAsync(payment);
        }

        return paymentItems;
    }

    private async Task<AccountingEntry> GenerateAccountingEntry(
        Payment payment,
        Member member,
        CreatePaymentCommand command,
        CancellationToken cancellationToken)
    {
        var entryNumber = await GenerateUniqueEntryNumber(command.CreatedBy!, cancellationToken);

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
            command.CreatedBy
        );

        await _accountingEntryRepository.AddAsync(accountingEntry, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Guardar para obtener ID

        // Generar items del asiento contable
        await GenerateAccountingEntryItems(accountingEntry, payment, member, command, cancellationToken);

        return accountingEntry;
    }

    private async Task GenerateAccountingEntryItems(
        AccountingEntry accountingEntry,
        Payment payment,
        Member member,
        CreatePaymentCommand command,
        CancellationToken cancellationToken)
    {
        // 1. DÉBITO: Caja/Bancos
        var debitAccountId = await GetCashAccountId(command.PaymentMethod!, cancellationToken);

        var debitItem = AccountingEntryItem.Create(
            accountingEntry.Id,
            debitAccountId,
            payment.TotalAmount,
            0.00M,
            $"Ingreso por pago - {command.PaymentMethod} - Comprobante: {payment.ReceiptNumber}",
            (int)EstadosEnum.Activo,
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            command.CreatedBy
        );

        await _entryItemRepository.AddAsync(debitItem, cancellationToken);

        // 2. CARGAR PAYMENT ITEMS (ya creados en este punto)
        var paymentItems = await LoadPaymentItemsForPayment(payment.Id, cancellationToken);

        var creditAccountIds = new HashSet<long>();

        // 3. CRÉDITOS: uno por cada PaymentItem según su FeeConfiguration
        foreach (var item in paymentItems)
        {
            var specParams = new BaseSpecParams { Id = item.MemberFeeId };
            var spec = new MemberFeeSpecification(specParams);
            var memberFee = await _memberFeeRepository.GetEntityWithSpec(spec, cancellationToken);

            long creditAccountId;

            if (memberFee?.FeeConfigurationId != null)
            {
                var feeConfig = await _feeConfigurationRepository.GetByIdAsync(memberFee.FeeConfigurationId, cancellationToken);
                creditAccountId = feeConfig?.IncomeAccountId ?? await GetAccountIdByCode("4.1.1.1", cancellationToken);

                var description = $"{feeConfig?.FeeName ?? "General"} - {member.FirstName} {member.LastName}";

                var creditItem = AccountingEntryItem.Create(
                    accountingEntry.Id,
                    creditAccountId,
                    0.00M,
                    item.Amount,
                    description,
                    (int)EstadosEnum.Activo,
                    _dateTimeProvider.CurrentTime.ToUniversalTime(),
                    command.CreatedBy
                );

                await _entryItemRepository.AddAsync(creditItem, cancellationToken);
            }
            else
            {
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
                    command.CreatedBy
                );

                await _entryItemRepository.AddAsync(creditItem, cancellationToken);
            }
            creditAccountIds.Add(creditAccountId);
        }

        _logger.LogInformation("Items contables generados: Débito en cuenta {DebitAccount}, Crédito en {CreditCount} cuentas",
            debitAccountId, creditAccountIds.Count);
    }

    private async Task<List<PaymentItem>> LoadPaymentItemsForPayment(long paymentId, CancellationToken cancellationToken)
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

            _logger.LogInformation("PaymentItems cargados: {Count} para PaymentId: {PaymentId}", items.Count, paymentId);
            return items.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar PaymentItems para payment {PaymentId}", paymentId);
            return new List<PaymentItem>();
        }
    }

    private async Task<string> GenerateUniqueEntryNumber(string createdBy, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.Today;
            var formattedDate = today.ToString("yyyyMMdd");

            var contador = await _contadorRepository.GetEntityWithSpec(new ContadorRawSpec("Pago"), cancellationToken);

            if (contador == null)
            {
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
        var accountCode = paymentMethod?.ToLower() switch
        {
            "cash" or "efectivo" => "1.1.1.1",
            "creditcard" or "debitcard" or "tarjeta" => "1.1.1.2",
            "transfer" or "transferencia" => "1.1.1.2",
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