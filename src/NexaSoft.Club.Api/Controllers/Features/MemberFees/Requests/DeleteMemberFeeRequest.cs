namespace NexaSoft.Club.Api.Controllers.Features.MemberFees.Request;

public sealed record DeleteMemberFeeRequest(
   long Id,
   string DeletedBy
);
