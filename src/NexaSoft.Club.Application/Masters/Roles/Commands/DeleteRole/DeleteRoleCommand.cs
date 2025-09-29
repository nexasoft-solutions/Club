using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Roles.Commands.DeleteRole;

public sealed record DeleteRoleCommand
(
    long Id
): ICommand<bool>;
