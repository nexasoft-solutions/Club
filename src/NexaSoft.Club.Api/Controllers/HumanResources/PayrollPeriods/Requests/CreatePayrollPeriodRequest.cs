namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods.Request;

public sealed record CreatePayrollPeriodRequest(
    long? PayrollTypeId,
    DateOnly? StartDate,
    DateOnly? EndDate,
    string CreatedBy
);
