using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.PayrollTypes;

public class PayrollType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public int StatePayrollType { get; private set; }

    private PayrollType() { }

    private PayrollType(
        string? code, 
        string? name, 
        string? description, 
        int statePayrollType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        StatePayrollType = statePayrollType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static PayrollType Create(
        string? code, 
        string? name, 
        string? description, 
        int statePayrollType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new PayrollType(
            code,
            name,
            description,
            statePayrollType,
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
        StatePayrollType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
