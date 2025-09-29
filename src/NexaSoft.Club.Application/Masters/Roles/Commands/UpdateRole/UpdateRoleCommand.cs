using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Roles.Commands.CreateRole;

public sealed record UpdateRoleCommand
(
    long Id,
    string? Name,
    string? Description,
    string? UsuarioModificacion
):ICommand<bool>;