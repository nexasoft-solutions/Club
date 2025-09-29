namespace NexaSoft.Club.Api.Controllers.Masters.MemberStatuses.Request;

public sealed record DeleteMemberStatusRequest(
   long Id,
   string DeletedBy
);
