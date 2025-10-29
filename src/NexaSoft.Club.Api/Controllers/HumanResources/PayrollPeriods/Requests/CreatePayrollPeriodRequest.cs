namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods.Request;

public sealed record CreatePayrollPeriodRequest(
    string? PeriodName,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    string CreatedBy
);
