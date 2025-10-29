namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptTypePayrolls.Request;

public sealed record CreateConceptTypePayrollRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
