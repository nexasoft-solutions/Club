using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Spaces.Commands.DeleteSpace;

public sealed record DeleteSpaceCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
