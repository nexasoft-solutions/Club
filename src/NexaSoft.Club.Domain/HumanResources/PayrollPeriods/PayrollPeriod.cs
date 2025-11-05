using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;
using NexaSoft.Club.Domain.HumanResources.PayrollTypes;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollPeriod : Entity
{
    public string? PeriodName { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public int? TotalEmployees { get; private set; }
    public long? StatusId { get; private set; }
    public PayrollPeriodStatus? Status { get; private set; }
    public long? PayrollTypeId { get; private set; }
    public PayrollType? PayrollType { get; private set; }
    public int StatePayrollPeriod { get; private set; }

    public virtual ICollection<PayrollDetail> PayrollDetails { get; private set; } = new List<PayrollDetail>();


    private PayrollPeriod() { }

    private PayrollPeriod(
        string? periodName,
        long? payrollTypeId,
        DateOnly? startDate,
        DateOnly? endDate,
        decimal totalAmount,
        int? totalEmployees,
        long? statusId,
        int statePayrollPeriod,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        PeriodName = periodName;
        PayrollTypeId = payrollTypeId;
        StartDate = startDate;
        EndDate = endDate;
        TotalAmount = totalAmount;
        TotalEmployees = totalEmployees;
        StatusId = statusId;
        StatePayrollPeriod = statePayrollPeriod;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollPeriod Create(
        string? periodName,
        long? payrollTypeId,
        DateOnly? startDate,
        DateOnly? endDate,
        decimal totalAmount,
        int? totalEmployees,
        long? statusId,
        int statePayrollPeriod,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollPeriod(
            periodName,
            payrollTypeId,
            startDate,
            endDate,
            totalAmount,
            totalEmployees,
            statusId,
            statePayrollPeriod,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? periodName,
        long? payrollTypeId,
        DateOnly? startDate,
        DateOnly? endDate,
        decimal totalAmount,
        int? totalEmployees,
        long? statusId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        PeriodName = periodName;
        PayrollTypeId = payrollTypeId;
        StartDate = startDate;
        EndDate = endDate;
        TotalAmount = totalAmount;
        TotalEmployees = totalEmployees;
        StatusId = statusId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StatePayrollPeriod = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }

    public void MarkAsCompleted()
    {
        // Lógica para marcar como completado
        this.StatusId = 3; // COMPLETADO
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        // Lógica para marcar como fallido
        this.StatusId = 4; // ERROR
        this.UpdatedAt = DateTime.UtcNow;
    }
}
