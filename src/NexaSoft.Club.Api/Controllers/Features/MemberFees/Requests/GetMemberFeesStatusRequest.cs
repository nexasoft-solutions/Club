namespace NexaSoft.Club.Api.Controllers.Features.MemberFees.Requests;

public sealed record GetMemberFeesStatusRequest
(
    long MemberId,
    long StatusId,
    string OrderBy 
);
