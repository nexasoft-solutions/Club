namespace NexaSoft.Club.Api.Controllers.HumanResources.ConceptCalculationTypes.Request;

public sealed record DeleteConceptCalculationTypeRequest(
   long Id,
   string DeletedBy
);
