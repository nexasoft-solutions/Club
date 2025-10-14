using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Commands.DeleteSpaceType;

public sealed record DeleteSpaceTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
