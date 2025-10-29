using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.HumanResources.Banks;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;
using NexaSoft.Club.Domain.HumanResources.Currencies;
using NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes;
using NexaSoft.Club.Domain.Masters.Statuses;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollPaymentRecord: Entity
{
    public long PayrollDetailId { get; private set; }
    public DateOnly PaymentDate { get; private set; }
    public long PaymentMethodId { get; private set; }
    public decimal Amount { get; private set; }
    public long CurrencyId { get; private set; }
    public decimal ExchangeRate { get; private set; }
    public string? Reference { get; private set; }
    public long? BankId { get; private set; }
    public long? CompanyBankAccountId { get; private set; }
    public long? StatusId { get; private set; }
    public string? PaymentFilePath { get; private set; }
    public string? ConfirmationDocumentPath { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public long? ProcessedById { get; private set; }

    // Relaciones
    public PayrollDetail? PayrollDetail { get; private set; }
    public PaymentMethodType? PaymentMethod { get; private set; }
    public Currency? Currency { get; private set; }
    public Bank? Bank { get; private set; }
    public CompanyBankAccount? CompanyBankAccount { get; private set; }
    public Status? Status { get; private set; }

    public int StatePayrollPaymentRecord { get; private set; }

    private PayrollPaymentRecord() { }

    private PayrollPaymentRecord(
        long payrollDetailId,
        DateOnly paymentDate,
        long paymentMethodId,
        decimal amount,
        long currencyId,
        decimal exchangeRate,
        string? reference,
        long? bankId,
        long? companyBankAccountId,
        long? statusId,
        string? paymentFilePath,
        string? confirmationDocumentPath,
        DateTime createdAt,
        string? createdBy,
        int statePayrollPaymentRecord
    ) : base(createdAt, createdBy)
    {
        PayrollDetailId = payrollDetailId;
        PaymentDate = paymentDate;
        PaymentMethodId = paymentMethodId;
        Amount = amount;
        CurrencyId = currencyId;
        ExchangeRate = exchangeRate;
        Reference = reference;
        BankId = bankId;
        CompanyBankAccountId = companyBankAccountId;
        StatusId = statusId;
        PaymentFilePath = paymentFilePath;
        ConfirmationDocumentPath = confirmationDocumentPath;
        StatePayrollPaymentRecord = statePayrollPaymentRecord;
    }

    public static PayrollPaymentRecord Create(
        long payrollDetailId,
        DateOnly paymentDate,
        long paymentMethodId,
        decimal amount,
        long currencyId,
        decimal exchangeRate,
        string? reference,
        long? bankId,
        long? companyBankAccountId,
        long? statusId,
        string? paymentFilePath,
        string? confirmationDocumentPath,
        DateTime createdAt,
        string? createdBy
    )
    {
        return new PayrollPaymentRecord(
            payrollDetailId,
            paymentDate,
            paymentMethodId,
            amount,
            currencyId,
            exchangeRate,
            reference,
            bankId,
            companyBankAccountId,
            statusId,
            paymentFilePath,
            confirmationDocumentPath,
            createdAt,
            createdBy,
            (int)EstadosEnum.Activo
        );
    }

   

    public Result MarkAsProcessed(DateTime processedAt, long processedById)
    {
        ProcessedAt = processedAt;
        ProcessedById = processedById;
        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StatePayrollPaymentRecord = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}