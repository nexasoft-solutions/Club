using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

public class PayrollPeriodStatus : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StatePayrollPeriodStatus { get; private set; }

    private PayrollPeriodStatus() { }

    private PayrollPeriodStatus(
        string? code, 
        string? name, 
        string? description, 
        int statePayrollPeriodStatus, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        StatePayrollPeriodStatus = statePayrollPeriodStatus;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollPeriodStatus Create(
        string? code, 
        string? name, 
        string? description, 
        int statePayrollPeriodStatus, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollPeriodStatus(
            code,
            name,
            description,
            statePayrollPeriodStatus,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePayrollPeriodStatus = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
