using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using NexaSoft.Club.Domain.Masters.SpaceTypes;
using NexaSoft.Club.Domain.Masters.SpacePhotos;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Domain.Masters.Spaces;

public class Space : Entity
{
    public string? SpaceName { get; private set; }

    public long SpaceTypeId { get; private set; }
    public SpaceType? SpaceType { get; private set; }
    public int? Capacity { get; private set; }
    public string? Description { get; private set; }
    public decimal StandardRate { get; private set; }
    public bool RequiresApproval { get; private set; }
    public int MaxReservationHours { get; private set; }
    public long? IncomeAccountId { get; private set; }
    public AccountingChart? IncomeAccount { get; private set; }
    public int StateSpace { get; private set; }

    public ICollection<SpacePhoto> SpacePhotos { get; private set; } = new List<SpacePhoto>();
    public ICollection<SpaceAvailability> SpaceAvailabilities { get; private set; } = new List<SpaceAvailability>();

    private Space() { }

    private Space(
        string? spaceName,
        long spaceTypeId,
        int? capacity,
        string? description,
        decimal standardRate,
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
        SpaceTypeId = spaceTypeId;
        Capacity = capacity;
        Description = description;
        StandardRate = standardRate;
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
        long spaceTypeId,
        int? capacity,
        string? description,
        decimal standardRate,
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
            spaceTypeId,
            capacity,
            description,
            standardRate,
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
        long spaceTypeId,
        int? capacity,
        string? description,
        decimal standardRate,
        bool requiresApproval,
        int maxReservationHours,
        long? incomeAccountId,
        DateTime utcNow,
        string? updatedBy
    )
    {
        SpaceName = spaceName;
        SpaceTypeId = spaceTypeId;
        Capacity = capacity;
        Description = description;
        StandardRate = standardRate;
        RequiresApproval = requiresApproval;
        MaxReservationHours = maxReservationHours;
        IncomeAccountId = incomeAccountId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateSpace = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
