namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollPeriods.Request;

public sealed record DeletePayrollPeriodRequest(
   long Id,
   string DeletedBy
);
