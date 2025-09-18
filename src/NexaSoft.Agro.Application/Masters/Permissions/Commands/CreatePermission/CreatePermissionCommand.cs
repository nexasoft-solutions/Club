using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Permissions.Commands.CreatePermision;

public sealed record CreatePermissionCommand
(
    string? Name,
    string? Description,
    string? ReferenciaControl,
    string? UsuarioCreacion
):ICommand<long>;