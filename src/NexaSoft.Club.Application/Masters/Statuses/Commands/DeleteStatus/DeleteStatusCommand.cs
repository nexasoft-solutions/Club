using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Statuses.Commands.DeleteStatus;

public sealed record DeleteStatusCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
