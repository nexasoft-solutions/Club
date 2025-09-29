using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Masters.Spaces;
using NexaSoft.Club.Domain.Masters.MemberTypes;

namespace NexaSoft.Club.Domain.Masters.SpaceRates;

public class SpaceRate : Entity
{
    public long SpaceId { get; private set; }
    public Space? Space { get; private set; }
    public long MemberTypeId { get; private set; }
    public MemberType? MemberType { get; private set; }
    public decimal Rate { get; private set; }
    public bool IsActive { get; private set; }
    public int StateSpaceRate { get; private set; }

    private SpaceRate() { }

    private SpaceRate(
        long spaceId, 
        long memberTypeId, 
        decimal rate, 
        bool isActive, 
        int stateSpaceRate, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        SpaceId = spaceId;
        MemberTypeId = memberTypeId;
        Rate = rate;
        IsActive = isActive;
        StateSpaceRate = stateSpaceRate;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static SpaceRate Create(
        long spaceId, 
        long memberTypeId, 
        decimal rate, 
        bool isActive, 
        int stateSpaceRate, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new SpaceRate(
            spaceId,
            memberTypeId,
            rate,
            isActive,
            stateSpaceRate,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long spaceId, 
        long memberTypeId, 
        decimal rate, 
        bool isActive, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        SpaceId = spaceId;
        MemberTypeId = memberTypeId;
        Rate = rate;
        IsActive = isActive;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateSpaceRate = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
