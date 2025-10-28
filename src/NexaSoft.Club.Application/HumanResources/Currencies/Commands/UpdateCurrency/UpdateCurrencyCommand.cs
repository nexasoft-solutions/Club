using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Commands.UpdateCurrency;

public sealed record UpdateCurrencyCommand(
    long Id,
    string? Code,
    string? Name,
    string UpdatedBy
) : ICommand<bool>;
