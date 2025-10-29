namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptTypePayrolls.Request;

public sealed record DeleteConceptTypePayrollRequest(
   long Id,
   string DeletedBy
);
