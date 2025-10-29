namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptTypePayrolls.Request;

public sealed record UpdateConceptTypePayrollRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
