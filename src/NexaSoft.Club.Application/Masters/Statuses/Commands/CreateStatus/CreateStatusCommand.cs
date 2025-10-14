using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Statuses.Commands.CreateStatus;

public sealed record CreateStatusCommand(
    string? Name,
    string? Description,
    string CreatedBy
) : ICommand<long>;
