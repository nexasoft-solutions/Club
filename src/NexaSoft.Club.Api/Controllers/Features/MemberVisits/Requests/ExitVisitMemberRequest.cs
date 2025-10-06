namespace NexaSoft.Club.Api.Controllers.Features.MemberVisits.Requests;

public sealed record ExitVisitMemberRequest
(
    long MemberId,
    string CheckOutBy,
    string? Notes
);