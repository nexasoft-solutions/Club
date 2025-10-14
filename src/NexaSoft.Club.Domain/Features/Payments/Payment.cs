using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Masters.PaymentTypes;
using NexaSoft.Club.Domain.Masters.DocumentTypes;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Domain.Features.Payments;

public class Payment : Entity
{
    public long MemberId { get; private set; }
    public Member? Member { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateOnly PaymentDate { get; private set; }
    public long PaymentMethodId { get; private set; }
    public PaymentType? PaymentType { get; private set; }
    public string? ReferenceNumber { get; private set; }
    public long DocumentTypeId { get; private set; }
    public DocumentType? DocumentType { get; private set; }
    public string? ReceiptNumber { get; private set; }
    public bool IsPartial { get; private set; }
    public long? AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public int StatePayment { get; private set; }

    public long StatusId { get; private set; }
    public Status? Status { get; private set; }

    public decimal CreditBalance { get; private set; }

    // Navegación a los items de pago
    public virtual ICollection<PaymentItem> PaymentItems { get; private set; } = new List<PaymentItem>();

    private Payment() { }

    private Payment(
        long memberId,
        decimal totalAmount,
        DateOnly paymentDate,
        long paymentMethodId,
        string? referenceNumber,
        long documentTypeId,
        string? receiptNumber,
        bool isPartial,
        long? accountingEntryId,
        int statePayment,
        DateTime createdAt,
        string? createdBy,
        long statusId,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        TotalAmount = totalAmount;
        PaymentDate = paymentDate;
        PaymentMethodId = paymentMethodId;
        ReferenceNumber = referenceNumber;
        DocumentTypeId = documentTypeId;
        ReceiptNumber = receiptNumber;
        IsPartial = isPartial;
        AccountingEntryId = accountingEntryId;
        StatePayment = statePayment;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        StatusId = statusId;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Payment Create(
        long memberId,
        decimal totalAmount,
        DateOnly paymentDate,
        long paymentMethodId,
        string? referenceNumber,
        long documentTypeId,
        string? receiptNumber,
        bool isPartial,
        long? accountingEntryId,
        int statePayment,
        DateTime createdAd,
        string? createdBy,
        long statusId
    )
    {
        var entity = new Payment(
            memberId,
            totalAmount,
            paymentDate,
            paymentMethodId,
            referenceNumber,
            documentTypeId,
            receiptNumber,
            isPartial,
            accountingEntryId,
            statePayment,
            createdAd,
            createdBy,
            statusId
        );
        return entity;
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


    public void MarkAsCompleted(long accountingEntryId)
    {
        SetAccountingEntryId(accountingEntryId);
        StatusId = (long)StatusEnum.Completado;
        UpdatedAt = DateTime.UtcNow;
        // Puedes agregar más lógica de estado si es necesario
    }

    public void MarkAsFailed()
    {
        StatusId = (long)StatusEnum.Fallido;
        //ErrorMessage = error;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        StatusId = (long)StatusEnum.Iniciado;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsActive()
    {
        StatusId = (long)StatusEnum.Activo;
        UpdatedAt = DateTime.UtcNow;
    }
}
