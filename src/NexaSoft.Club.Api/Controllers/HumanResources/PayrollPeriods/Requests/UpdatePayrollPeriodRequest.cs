namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods.Request;

public sealed record UpdatePayrollPeriodRequest(
    long Id,
    string? PeriodName,
    long? PayrollTypeId,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal TotalAmount,
    int? TotalEmployees,
    long? StatusId,
    string UpdatedBy
);
