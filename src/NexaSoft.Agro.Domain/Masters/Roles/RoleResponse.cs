namespace NexaSoft.Agro.Domain.Masters.Roles;

public sealed record RoleResponse
(
    long Id,
    string? Name,
    string? Description,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
