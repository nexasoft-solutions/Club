using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.Members;
using NexaSoft.Club.Domain.Masters.FeeConfigurations;

namespace NexaSoft.Club.Domain.Features.MemberFees;

public class MemberFee : Entity
{
    public long MemberId { get; private set; }
    public Member? Member { get; private set; }
    public long? MemberTypeFeeId { get; private set; }
    public MemberTypeFee? MemberTypeFee { get; private set; }
    public string? Period { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly DueDate { get; private set; }
    public string? Status { get; private set; }
    public decimal PaidAmount { get; private set; }
    public decimal RemainingAmount { get; private set; }
    public int StateMemberFee { get; private set; }

    private MemberFee() { }

    private MemberFee(
        long memberId,
        long? configId,
        string? period,
        decimal amount,
        DateOnly dueDate,
        string? status,
        decimal paidAmount,
        decimal remainingAmount,
        int stateMemberFee,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        MemberId = memberId;
        MemberTypeFeeId = configId;
        Period = period;
        Amount = amount;
        DueDate = dueDate;
        Status = status;
        PaidAmount = paidAmount;
        RemainingAmount = remainingAmount;
        StateMemberFee = stateMemberFee;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static MemberFee Create(
        long memberId,
        long? configId,
        string? period,
        decimal amount,
        DateOnly dueDate,
        string? status,
        decimal paidAmount,
        decimal remainingAmount,
        int stateMemberFee,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new MemberFee(
            memberId,
            configId,
            period,
            amount,
            dueDate,
            status,
            paidAmount,
            remainingAmount,
            stateMemberFee,
            createdAd,
            createdBy
        );
        return entity;
    }

    // MÉTODO NUEVO: ApplyPayment
    public void ApplyPayment(decimal paymentAmount, DateTime paymentDate, string processedBy)
    {
        if (paymentAmount <= 0)
            throw new ArgumentException("El monto del pago debe ser mayor a cero", nameof(paymentAmount));

        if (paymentAmount > RemainingAmount)
            throw new InvalidOperationException($"El monto del pago ({paymentAmount}) excede el saldo pendiente ({RemainingAmount})");

        // Actualizar montos
        PaidAmount += paymentAmount;
        RemainingAmount = Amount - PaidAmount;

        // Actualizar estado
        UpdateStatusBasedOnPayment();

        // Actualizar auditoría
        UpdatedAt = paymentDate;
        UpdatedBy = processedBy;

        // Podrías agregar aquí un Domain Event si necesitas notificar otros sistemas
        // RaiseDomainEvent(new MemberFeePaymentAppliedDomainEvent(Id, paymentAmount, paymentDate));
    }

    // MÉTODO PARA ACTUALIZAR ESTADO BASADO EN EL PAGO
    private void UpdateStatusBasedOnPayment()
    {
        if (RemainingAmount <= 0)
        {
            Status = "Pagado";
        }
        else if (PaidAmount > 0)
        {
            Status = "Parcial";
        }
        else
        {
            Status = "Pendiente";
        }
    }

    // MÉTODO PARA PAGO COMPLETO (conveniencia)
    public void MarkAsPaid(DateTime paymentDate, string processedBy)
    {
        ApplyPayment(RemainingAmount, paymentDate, processedBy);
    }

    // MÉTODO PARA REVERTIR PAGO (si es necesario)
    public void ReversePayment(decimal amountToReverse, DateTime reversalDate, string processedBy)
    {
        if (amountToReverse <= 0)
            throw new ArgumentException("El monto a revertir debe ser mayor a cero", nameof(amountToReverse));

        if (amountToReverse > PaidAmount)
            throw new InvalidOperationException($"El monto a revertir ({amountToReverse}) excede el monto pagado ({PaidAmount})");

        PaidAmount -= amountToReverse;
        RemainingAmount = Amount - PaidAmount;

        UpdateStatusBasedOnPayment();

        UpdatedAt = reversalDate;
        UpdatedBy = processedBy;
    }

    public Result Update(
        long Id,
        long memberId,
        long? configId,
        string? period,
        decimal amount,
        DateOnly dueDate,
        string? status,
        decimal paidAmount,
        decimal remainingAmount,
        DateTime utcNow,
        string? updatedBy
    )
    {
        MemberId = memberId;
        MemberTypeFeeId = configId;
        Period = period;
        Amount = amount;
        DueDate = dueDate;
        Status = status;
        PaidAmount = paidAmount;
        RemainingAmount = remainingAmount;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateMemberFee = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    // Domain/Features/MemberFees/MemberFee.cs (agregar método)
    public void UpdateStatus(string newStatus, DateTime updatedAt, string updatedBy)
    {
        Status = newStatus;
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
}
