namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollFormulas.Request;

public sealed record DeletePayrollFormulaRequest(
   long Id,
   string DeletedBy
);
