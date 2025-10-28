using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

public class EmployeeType : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal BaseSalary { get; private set; }
    public int StateEmployeeType { get; private set; }

    private EmployeeType() { }

    private EmployeeType(
        string? code, 
        string? name, 
        string? description, 
        decimal baseSalary, 
        int stateEmployeeType, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        Description = description;
        BaseSalary = baseSalary;
        StateEmployeeType = stateEmployeeType;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static EmployeeType Create(
        string? code, 
        string? name, 
        string? description, 
        decimal baseSalary, 
        int stateEmployeeType, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new EmployeeType(
            code,
            name,
            description,
            baseSalary,
            stateEmployeeType,
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
        decimal baseSalary, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        Description = description;
        BaseSalary = baseSalary;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateEmployeeType = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
