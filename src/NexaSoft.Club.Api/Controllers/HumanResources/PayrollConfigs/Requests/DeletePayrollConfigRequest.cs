namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConfigs.Request;

public sealed record DeletePayrollConfigRequest(
   long Id,
   string DeletedBy
);
