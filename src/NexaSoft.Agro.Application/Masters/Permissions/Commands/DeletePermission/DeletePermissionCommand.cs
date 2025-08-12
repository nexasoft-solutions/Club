using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.DeletePermission;

public sealed record DeletePermissionCommand
(
    Guid Id
): ICommand<bool>;
