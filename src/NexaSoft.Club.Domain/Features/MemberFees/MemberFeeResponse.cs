namespace NexaSoft.Club.Domain.Features.MemberFees;

public sealed record MemberFeeResponse(
    long Id,
    long MemberId,
    string? MemberFirstName,
    string? MemberLastName,
    long? MemberTypeFeeId,    
    string? FeeName,
    long FeeConfigurationId,
    string? Period,
    decimal Amount,
    decimal RemainingAmount,
    decimal PaidAmount,
    DateOnly DueDate,
    long StatusId,
    string StatusName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
