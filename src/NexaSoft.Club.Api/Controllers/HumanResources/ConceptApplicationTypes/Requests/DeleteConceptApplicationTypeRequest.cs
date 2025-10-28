namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptApplicationTypes.Request;

public sealed record DeleteConceptApplicationTypeRequest(
   long Id,
   string DeletedBy
);
