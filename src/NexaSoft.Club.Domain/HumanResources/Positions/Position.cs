using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.Departments;
using NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

namespace NexaSoft.Club.Domain.HumanResources.Positions;

public class Position : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long? DepartmentId { get; private set; }
    public Department? Department { get; private set; }
    public long? EmployeeTypeId { get; private set; }
    public EmployeeType? EmployeeType { get; private set; }
    public decimal BaseSalary { get; private set; }
    public string? Description { get; private set; }
    public int StatePosition { get; private set; }

    private Position() { }

    private Position(
        string? code, 
        string? name, 
        long? departmentId, 
        long? employeeTypeId, 
        decimal baseSalary, 
        string? description, 
        int statePosition, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        DepartmentId = departmentId;
        EmployeeTypeId = employeeTypeId;
        BaseSalary = baseSalary;
        Description = description;
        StatePosition = statePosition;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Position Create(
        string? code, 
        string? name, 
        long? departmentId, 
        long? employeeTypeId, 
        decimal baseSalary, 
        string? description, 
        int statePosition, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Position(
            code,
            name,
            departmentId,
            employeeTypeId,
            baseSalary,
            description,
            statePosition,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        long? departmentId, 
        long? employeeTypeId, 
        decimal baseSalary, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        DepartmentId = departmentId;
        EmployeeTypeId = employeeTypeId;
        BaseSalary = baseSalary;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StatePosition = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
