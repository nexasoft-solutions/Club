using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Permissions.Commands.CreatePermission;

public sealed record UpdatePermissionCommand
(
    long Id,
    string? Name,
    string? Description,
    string? ReferenciaControl,
    string? UsuarioModificacion
):ICommand<bool>;