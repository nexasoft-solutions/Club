namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollTypes.Request;

public sealed record DeletePayrollTypeRequest(
   long Id,
   string DeletedBy
);
