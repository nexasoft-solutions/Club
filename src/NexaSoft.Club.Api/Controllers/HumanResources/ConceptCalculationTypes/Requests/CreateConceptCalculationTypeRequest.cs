namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptCalculationTypes.Request;

public sealed record CreateConceptCalculationTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
