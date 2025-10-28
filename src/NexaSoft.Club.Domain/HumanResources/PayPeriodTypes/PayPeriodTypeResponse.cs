namespace NexaSoft.Club.Domain.HumanResources.PayPeriodTypes;

public sealed record PayPeriodTypeResponse(
    long Id,
    string? Code,
    string? Name,
    int? Days,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
