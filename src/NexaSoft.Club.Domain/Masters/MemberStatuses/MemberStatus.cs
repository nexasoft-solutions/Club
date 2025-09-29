using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.MemberStatuses;

public class MemberStatus : Entity
{
    public string? StatusName { get; private set; }
    public string? Description { get; private set; }
    public bool CanAccess { get; private set; }
    public bool CanReserve { get; private set; }
    public bool CanParticipate { get; private set; }
    public int StateMemberStatus { get; private set; }

    private MemberStatus() { }

    private MemberStatus(
        string? statusName, 
        string? description, 
        bool canAccess, 
        bool canReserve, 
        bool canParticipate, 
        int stateMemberStatus, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        StatusName = statusName;
        Description = description;
        CanAccess = canAccess;
        CanReserve = canReserve;
        CanParticipate = canParticipate;
        StateMemberStatus = stateMemberStatus;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static MemberStatus Create(
        string? statusName, 
        string? description, 
        bool canAccess, 
        bool canReserve, 
        bool canParticipate, 
        int stateMemberStatus, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new MemberStatus(
            statusName,
            description,
            canAccess,
            canReserve,
            canParticipate,
            stateMemberStatus,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? statusName, 
        string? description, 
        bool canAccess, 
        bool canReserve, 
        bool canParticipate, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        StatusName = statusName;
        Description = description;
        CanAccess = canAccess;
        CanReserve = canReserve;
        CanParticipate = canParticipate;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateMemberStatus = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
