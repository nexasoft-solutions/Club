using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Commands.DeleteCurrency;

public sealed record DeleteCurrencyCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
