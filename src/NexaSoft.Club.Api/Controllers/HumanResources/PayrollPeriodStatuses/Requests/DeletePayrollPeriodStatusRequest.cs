namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriodStatuses.Request;

public sealed record DeletePayrollPeriodStatusRequest(
   long Id,
   string DeletedBy
);
