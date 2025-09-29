namespace NexaSoft.Club.Api.Controllers.Masters.MemberStatuses.Request;

public sealed record CreateMemberStatusRequest(
    string? StatusName,
    string? Description,
    bool CanAccess,
    bool CanReserve,
    bool CanParticipate,
    string CreatedBy
);
