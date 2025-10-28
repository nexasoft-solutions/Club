namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptCalculationTypes.Request;

public sealed record UpdateConceptCalculationTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
