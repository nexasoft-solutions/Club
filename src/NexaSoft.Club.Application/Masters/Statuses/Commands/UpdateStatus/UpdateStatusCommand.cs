using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Statuses.Commands.UpdateStatus;

public sealed record UpdateStatusCommand(
    long Id,
    string? Name,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
