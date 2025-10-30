namespace NexaSoft.Club.Api.Controllers.HumanResources.IncomeTaxScales.Request;

public sealed record DeleteIncomeTaxScaleRequest(
   long Id,
   string DeletedBy
);
