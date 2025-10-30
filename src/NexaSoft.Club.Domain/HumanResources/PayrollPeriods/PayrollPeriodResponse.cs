namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods;

public sealed record PayrollPeriodResponse(
    long Id,
    string? PeriodName,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    string? StatusCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
