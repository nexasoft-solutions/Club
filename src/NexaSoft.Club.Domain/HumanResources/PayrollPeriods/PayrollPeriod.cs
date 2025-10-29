using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.Statuses;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public class PayrollPeriod : Entity
{
    public string? PeriodName { get; private set; }
    public DateOnly? StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public decimal TotalAmount { get; private set; }
    public int? TotalEmployees { get; private set; }
    public long? StatusId { get; private set; }
    public Status? Status { get; private set; }
    public int StatePayrollPeriod { get; private set; }

    private PayrollPeriod() { }

    private PayrollPeriod(
        string? periodName, 
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
        StartDate = startDate;
        EndDate = endDate;
        TotalAmount = totalAmount;
        TotalEmployees = totalEmployees;
        StatusId = statusId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollPeriod = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
