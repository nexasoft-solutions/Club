using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Commands.DeleteSpecialRate;

public sealed record DeleteSpecialRateCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
