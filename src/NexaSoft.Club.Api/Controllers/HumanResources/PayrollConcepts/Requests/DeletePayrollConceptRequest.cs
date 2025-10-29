namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConcepts.Request;

public sealed record DeletePayrollConceptRequest(
   long Id,
   string DeletedBy
);
