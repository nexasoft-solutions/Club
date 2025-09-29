namespace NexaSoft.Club.Api.Controllers.Features.Members.Request;

public sealed record DeleteMemberRequest(
   long Id,
   string DeletedBy
);
