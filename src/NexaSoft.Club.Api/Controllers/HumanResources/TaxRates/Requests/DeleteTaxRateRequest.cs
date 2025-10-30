namespace NexaSoft.Club.Api.Controllers.HumanResources.TaxRates.Request;

public sealed record DeleteTaxRateRequest(
   long Id,
   string DeletedBy
);
