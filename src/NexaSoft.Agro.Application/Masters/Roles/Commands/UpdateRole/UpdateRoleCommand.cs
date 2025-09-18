using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Masters.Roles.Commands.CreateRole;

public sealed record UpdateRoleCommand
(
    long Id,
    string? Name,
    string? Description,
    string? UsuarioModificacion
):ICommand<bool>;