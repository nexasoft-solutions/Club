namespace NexaSoft.Club.Domain.HumanResources.TaxRates;

public sealed record TaxRateResponse(
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
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
