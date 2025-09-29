namespace NexaSoft.Club.Api.Controllers.Features.MemberFees.Request;

public sealed record CreateMemberFeeRequest(
    long MemberId,
    long? ConfigId,
    string? Period,
    decimal Amount,
    DateOnly DueDate,
    string? Status,
    string CreatedBy
);
