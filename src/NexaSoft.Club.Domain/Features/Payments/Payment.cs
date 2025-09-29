using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.AccountingEntries;

namespace NexaSoft.Club.Domain.Features.Payments;

public class Payment : Entity
{
    public long MemberId { get; private set; }
    public Member? Member { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateOnly PaymentDate { get; private set; }
    public string? PaymentMethod { get; private set; }
    public string? ReferenceNumber { get; private set; }
    public string? ReceiptNumber { get; private set; }
    public bool IsPartial { get; private set; }
    public long? AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public int StatePayment { get; private set; }

    public decimal CreditBalance { get; private set; }

    // Navegación a los items de pago
    public virtual ICollection<PaymentItem> PaymentItems { get; private set; } = new List<PaymentItem>();

    private Payment() { }

    private Payment(
        long memberId,
        decimal totalAmount,
        DateOnly paymentDate,
        string? paymentMethod,
        string? referenceNumber,
        string? receiptNumber,
        bool isPartial,
        long? accountingEntryId,
        int statePayment,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        TotalAmount = totalAmount;
        PaymentDate = paymentDate;
        PaymentMethod = paymentMethod;
        ReferenceNumber = referenceNumber;
        ReceiptNumber = receiptNumber;
        IsPartial = isPartial;
        AccountingEntryId = accountingEntryId;
        StatePayment = statePayment;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Payment Create(
        long memberId,
        decimal totalAmount,
        DateOnly paymentDate,
        string? paymentMethod,
        string? referenceNumber,
        string? receiptNumber,
        bool isPartial,
        long? accountingEntryId,
        int statePayment,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Payment(
            memberId,
            totalAmount,
            paymentDate,
            paymentMethod,
            referenceNumber,
            receiptNumber,
            isPartial,
            accountingEntryId,
            statePayment,
            createdAd,
            createdBy
        );
        return entity;
    }


    public Result Update(
        long Id,
        long memberId,
        decimal totalAmount,
        DateOnly paymentDate,
        string? paymentMethod,
        string? referenceNumber,
        string? receiptNumber,
        bool isPartial,
        long? accountingEntryId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        MemberId = memberId;
        TotalAmount = totalAmount;
        PaymentDate = paymentDate;
        PaymentMethod = paymentMethod;
        ReferenceNumber = referenceNumber;
        ReceiptNumber = receiptNumber;
        IsPartial = isPartial;
        AccountingEntryId = accountingEntryId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public void SetAccountingEntryId(long accountingEntryId)
    {
        AccountingEntryId = accountingEntryId;
        UpdatedAt = DateTime.UtcNow;
    }

    // MÉTODO PARA CALCULAR EL TOTAL A PARTIR DE LOS ITEMS
    public decimal CalculateTotalFromItems()
    {
        return PaymentItems.Sum(pi => pi.Amount);
    }

    // MÉTODO PARA VALIDAR QUE EL TOTAL COINCIDE CON LOS ITEMS
    public Result ValidateTotalAmount()
    {
        var calculatedTotal = CalculateTotalFromItems();

        if (TotalAmount != calculatedTotal)
            return Result.Failure(new Error("Payment.SumaItemNoCoincide", $"El total del pago ({TotalAmount}) no coincide con la suma de los items ({calculatedTotal})"));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StatePayment = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void AddCreditBalance(decimal amount)
    {
        if (amount <= 0) return;

        CreditBalance += amount;
        UpdatedAt = DateTime.UtcNow;
    }
}
