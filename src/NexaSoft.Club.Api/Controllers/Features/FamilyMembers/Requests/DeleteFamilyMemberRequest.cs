namespace NexaSoft.Club.Api.Controllers.Features.FamilyMembers.Request;

public sealed record DeleteFamilyMemberRequest(
   long Id,
   string DeletedBy
);
