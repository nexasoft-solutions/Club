namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptApplicationTypes.Request;

public sealed record CreateConceptApplicationTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
