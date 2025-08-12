using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.CreatePermission;

public sealed record UpdatePermissionCommand
(
    Guid Id,
    string? Name,
    string? Description,
    string? ReferenciaControl
):ICommand<bool>;