using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Features.MemberFees;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Features.Payments;

public class PaymentItem: Entity
{
    public long PaymentId { get; private set; }
    public Payment? Payment { get; private set; }
    public long MemberFeeId { get; private set; }
    public MemberFee? MemberFee { get; private set; }
    public decimal Amount { get; private set; }
    public int StatePaymentItem { get; private set; }

    private PaymentItem() { }

    private PaymentItem(
        long paymentId,
        long memberFeeId,
        decimal amount,
        int statePaymentItem,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        PaymentId = paymentId;
        MemberFeeId = memberFeeId;
        Amount = amount;
        StatePaymentItem = statePaymentItem;
    }

    public static PaymentItem Create(
        long paymentId,
        long memberFeeId,
        decimal amount,
        int statePaymentItem,
        DateTime createdAt,
        string? createdBy
    )
    {
        if (amount <= 0)
            throw new ArgumentException("El monto debe ser mayor a cero", nameof(amount));

        var entity = new PaymentItem(
            paymentId,
            memberFeeId,
            amount,
            statePaymentItem,
            createdAt,
            createdBy
        );
        return entity;
    }

    public Result Update(
        decimal amount,
        DateTime utcNow,
        string? updatedBy
    )
    {
        if (amount <= 0)
            return Result.Failure(PaymentErrores.MontoMayorCero);

        Amount = amount;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StatePaymentItem = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}