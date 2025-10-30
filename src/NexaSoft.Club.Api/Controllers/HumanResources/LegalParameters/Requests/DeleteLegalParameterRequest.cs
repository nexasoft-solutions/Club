namespace NexaSoft.Club.Api.Controllers.HumanResources.LegalParameters.Request;

public sealed record DeleteLegalParameterRequest(
   long Id,
   string DeletedBy
);
