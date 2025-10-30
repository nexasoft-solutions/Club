using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Domain.HumanResources.CostCenters;

public class CostCenter : Entity
{
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public long? ParentCostCenterId { get; private set; }
    public CostCenter? ParentCostCenter { get; private set; }
    public long? CostCenterTypeId { get; private set; }
    public CostCenterType? CostCenterType { get; private set; }
    public string? Description { get; private set; }
    public long? ResponsibleId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public decimal Budget { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public int StateCostCenter { get; private set; }

    private CostCenter() { }

    private CostCenter(
        string? code, 
        string? name, 
        long? parentCostCenterId, 
        long? costCenterTypeId, 
        string? description, 
        long? responsibleId, 
        decimal budget, 
        DateOnly startDate, 
        DateOnly? endDate, 
        int stateCostCenter, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        Code = code;
        Name = name;
        ParentCostCenterId = parentCostCenterId;
        CostCenterTypeId = costCenterTypeId;
        Description = description;
        ResponsibleId = responsibleId;
        Budget = budget;
        StartDate = startDate;
        EndDate = endDate;
        StateCostCenter = stateCostCenter;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static CostCenter Create(
        string? code, 
        string? name, 
        long? parentCostCenterId, 
        long? costCenterTypeId, 
        string? description, 
        long? responsibleId, 
        decimal budget, 
        DateOnly startDate, 
        DateOnly? endDate, 
        int stateCostCenter, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new CostCenter(
            code,
            name,
            parentCostCenterId,
            costCenterTypeId,
            description,
            responsibleId,
            budget,
            startDate,
            endDate,
            stateCostCenter,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? code, 
        string? name, 
        long? parentCostCenterId, 
        long? costCenterTypeId, 
        string? description, 
        long? responsibleId, 
        decimal budget, 
        DateOnly startDate, 
        DateOnly? endDate, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        Code = code;
        Name = name;
        ParentCostCenterId = parentCostCenterId;
        CostCenterTypeId = costCenterTypeId;
        Description = description;
        ResponsibleId = responsibleId;
        Budget = budget;
        StartDate = startDate;
        EndDate = endDate;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateCostCenter = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
