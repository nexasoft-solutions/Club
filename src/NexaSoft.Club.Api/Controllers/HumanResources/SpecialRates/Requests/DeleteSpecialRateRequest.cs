namespace NexaSoft.Club.Api.Controllers.HumanResources.SpecialRates.Request;

public sealed record DeleteSpecialRateRequest(
   long Id,
   string DeletedBy
);
