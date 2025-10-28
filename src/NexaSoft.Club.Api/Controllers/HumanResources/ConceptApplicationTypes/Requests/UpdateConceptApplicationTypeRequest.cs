namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptApplicationTypes.Request;

public sealed record UpdateConceptApplicationTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
