using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.DeleteSpaceAvailability;

public sealed record DeleteSpaceAvailabilityCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
