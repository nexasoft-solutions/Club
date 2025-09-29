using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.DeletePermission;

public sealed record DeletePermissionCommand
(
    long Id
): ICommand<bool>;
