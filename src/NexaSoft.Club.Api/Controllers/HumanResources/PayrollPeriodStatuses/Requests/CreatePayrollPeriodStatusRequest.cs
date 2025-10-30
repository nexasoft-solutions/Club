namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriodStatuses.Request;

public sealed record CreatePayrollPeriodStatusRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
