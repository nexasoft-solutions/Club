namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods.Request;

public sealed record CreatePayrollPeriodRequest(
    string? PeriodName,
    DateOnly? StartDate,
    DateOnly? EndDate,    
    string CreatedBy
);
