using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Domain.HumanResources.Departments;

public class Department : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long? ParentDepartmentId { get; private set; }
    public Department? ParentDepartment { get; private set; }
    public string? Description { get; private set; }
    public long? ManagerId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }
    public string? Location { get; private set; }
    public string? PhoneExtension { get; private set; }
    public int StateDepartment { get; private set; }

    private Department() { }

    private Department(
        string? code, 
        string? name, 
        long? parentDepartmentId, 
        string? description, 
        long? managerId, 
        long? costCenterId, 
        string? location, 
        string? phoneExtension, 
        int stateDepartment, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        ParentDepartmentId = parentDepartmentId;
        Description = description;
        ManagerId = managerId;
        CostCenterId = costCenterId;
        Location = location;
        PhoneExtension = phoneExtension;
        StateDepartment = stateDepartment;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Department Create(
        string? code, 
        string? name, 
        long? parentDepartmentId, 
        string? description, 
        long? managerId, 
        long? costCenterId, 
        string? location, 
        string? phoneExtension, 
        int stateDepartment, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Department(
            code,
            name,
            parentDepartmentId,
            description,
            managerId,
            costCenterId,
            location,
            phoneExtension,
            stateDepartment,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        long? parentDepartmentId, 
        string? description, 
        long? managerId, 
        long? costCenterId, 
        string? location, 
        string? phoneExtension, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        ParentDepartmentId = parentDepartmentId;
        Description = description;
        ManagerId = managerId;
        CostCenterId = costCenterId;
        Location = location;
        PhoneExtension = phoneExtension;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateDepartment = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
