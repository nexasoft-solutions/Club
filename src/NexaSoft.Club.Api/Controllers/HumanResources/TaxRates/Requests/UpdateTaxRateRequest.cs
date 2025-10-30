namespace NexaSoft.Club.Api.Controllers.HumanResources.TaxRates.Request;

public sealed record UpdateTaxRateRequest(
   long Id,
    string? Code,
    string? Name,
    decimal RateValue,
    string? RateType,
    decimal? MinAmount,
    decimal? MaxAmount,
    DateOnly EffectiveDate,
    DateOnly? EndDate,
    string? Category,
    string? Description,
    string? AppliesTo,
    string UpdatedBy
);
