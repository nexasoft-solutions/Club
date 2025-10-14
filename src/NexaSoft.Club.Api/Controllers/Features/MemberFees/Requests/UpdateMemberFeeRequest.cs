namespace NexaSoft.Club.Api.Controllers.Features.MemberFees.Request;

public sealed record UpdateMemberFeeRequest(
   long Id,
    long MemberId,
    long?ConfigId,
    string? Period,
    decimal Amount,
    DateOnly DueDate,
    long StatusId,
    string UpdatedBy
);
