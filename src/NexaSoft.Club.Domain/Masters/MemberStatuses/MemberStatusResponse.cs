namespace NexaSoft.Club.Domain.Masters.MemberStatuses;

public sealed record MemberStatusResponse(
    long Id,
    string? StatusName,
    string? Description,
    bool CanAccess,
    bool CanReserve,
    bool CanParticipate,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
