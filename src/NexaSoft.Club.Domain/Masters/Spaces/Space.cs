using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Domain.Masters.Spaces;

public class Space : Entity
{
    public string? SpaceName { get; private set; }
    public string? SpaceType { get; private set; }
    public int? Capacity { get; private set; }
    public string? Description { get; private set; }
    public decimal StandardRate { get; private set; }
    public bool IsActive { get; private set; }
    public bool RequiresApproval { get; private set; }
    public int MaxReservationHours { get; private set; }
    public long? IncomeAccountId { get; private set; }
    public AccountingChart? IncomeAccount { get; private set; }
    public int StateSpace { get; private set; }

    private Space() { }

    private Space(
        string? spaceName, 
        string? spaceType, 
        int? capacity, 
        string? description, 
        decimal standardRate, 
        bool isActive, 
        bool requiresApproval, 
        int maxReservationHours, 
        long? incomeAccountId, 
        int stateSpace, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        SpaceName = spaceName;
        SpaceType = spaceType;
        Capacity = capacity;
        Description = description;
        StandardRate = standardRate;
        IsActive = isActive;
        RequiresApproval = requiresApproval;
        MaxReservationHours = maxReservationHours;
        IncomeAccountId = incomeAccountId;
        StateSpace = stateSpace;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Space Create(
        string? spaceName, 
        string? spaceType, 
        int? capacity, 
        string? description, 
        decimal standardRate, 
        bool isActive, 
        bool requiresApproval, 
        int maxReservationHours, 
        long? incomeAccountId, 
        int stateSpace, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Space(
            spaceName,
            spaceType,
            capacity,
            description,
            standardRate,
            isActive,
            requiresApproval,
            maxReservationHours,
            incomeAccountId,
            stateSpace,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? spaceName, 
        string? spaceType, 
        int? capacity, 
        string? description, 
        decimal standardRate, 
        bool isActive, 
        bool requiresApproval, 
        int maxReservationHours, 
        long? incomeAccountId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        SpaceName = spaceName;
        SpaceType = spaceType;
        Capacity = capacity;
        Description = description;
        StandardRate = standardRate;
        IsActive = isActive;
        RequiresApproval = requiresApproval;
        MaxReservationHours = maxReservationHours;
        IncomeAccountId = incomeAccountId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSpace = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
