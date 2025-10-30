namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses;

public sealed record PayrollPeriodStatusResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
