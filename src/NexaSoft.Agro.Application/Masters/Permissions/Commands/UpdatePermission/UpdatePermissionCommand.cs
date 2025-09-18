using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.CreatePermission;

public sealed record UpdatePermissionCommand
(
    long Id,
    string? Name,
    string? Description,
    string? ReferenciaControl,
    string? UsuarioModificacion
):ICommand<bool>;