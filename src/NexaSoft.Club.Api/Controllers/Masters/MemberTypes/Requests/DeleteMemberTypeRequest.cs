namespace NexaSoft.Club.Api.Controllers.Masters.MemberTypes.Request;

public sealed record DeleteMemberTypeRequest(
   long Id,
   string DeletedBy
);
