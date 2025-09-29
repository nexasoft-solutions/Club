using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Periodicities.Commands.DeletePeriodicity;

public sealed record DeletePeriodicityCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
