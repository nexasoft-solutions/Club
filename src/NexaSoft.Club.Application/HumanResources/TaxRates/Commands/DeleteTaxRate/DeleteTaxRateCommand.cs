using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.DeleteTaxRate;

public sealed record DeleteTaxRateCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
