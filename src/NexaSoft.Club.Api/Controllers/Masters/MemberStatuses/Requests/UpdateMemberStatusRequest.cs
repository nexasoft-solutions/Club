namespace NexaSoft.Club.Api.Controllers.Masters.MemberStatuses.Request;

public sealed record UpdateMemberStatusRequest(
   long Id,
    string? StatusName,
    string? Description,
    bool CanAccess,
    bool CanReserve,
    bool CanParticipate,
    string UpdatedBy
);
