namespace NexaSoft.Club.Api.Controllers.Features.MemberVisits.Request;

public sealed record DeleteMemberVisitRequest(
   long Id,
   string DeletedBy
);
