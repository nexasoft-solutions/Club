namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriodStatuses.Request;

public sealed record UpdatePayrollPeriodStatusRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
