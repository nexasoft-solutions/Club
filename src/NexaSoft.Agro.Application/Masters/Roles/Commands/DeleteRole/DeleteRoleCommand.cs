using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Roles.Commands.DeleteRole;

public sealed record DeleteRoleCommand
(
    long Id
): ICommand<bool>;
