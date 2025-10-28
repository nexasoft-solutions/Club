using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Commands.CreateCurrency;

public sealed record CreateCurrencyCommand(
    string? Code,
    string? Name,
    string CreatedBy
) : ICommand<long>;
