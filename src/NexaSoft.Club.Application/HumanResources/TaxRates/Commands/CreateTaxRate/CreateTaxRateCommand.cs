using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.CreateTaxRate;

public sealed record CreateTaxRateCommand(
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
    string CreatedBy
) : ICommand<long>;
