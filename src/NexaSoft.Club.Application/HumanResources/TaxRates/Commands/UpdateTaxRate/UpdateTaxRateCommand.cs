using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.UpdateTaxRate;

public sealed record UpdateTaxRateCommand(
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
) : ICommand<bool>;
