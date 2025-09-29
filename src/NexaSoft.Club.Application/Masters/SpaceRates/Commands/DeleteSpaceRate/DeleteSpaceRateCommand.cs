using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Commands.DeleteSpaceRate;

public sealed record DeleteSpaceRateCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
