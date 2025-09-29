
namespace NexaSoft.Club.Domain.Masters.FeeConfigurations;

public sealed record MemberTypeFeeResponse
(
    long Id,
    long MemberTypeId,
    long FeeConfigurationId,
    decimal? Amount,
    decimal? DefaultAmount,
    string FeeName,
    long PeriodicityId,
    int? DueDay,
    int Priority,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);