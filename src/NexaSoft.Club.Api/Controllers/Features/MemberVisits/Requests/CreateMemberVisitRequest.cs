namespace NexaSoft.Club.Api.Controllers.Features.MemberVisits.Request;

public sealed record CreateMemberVisitRequest(
    long MemberId,
    string? QrCodeUsed,
    string? Notes,
    string CreatedBy
);
