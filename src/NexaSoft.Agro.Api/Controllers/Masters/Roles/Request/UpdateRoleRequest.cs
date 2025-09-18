namespace NexaSoft.Agro.Api.Controllers.Masters.Roles.Request;

public record class UpdateRoleRequest
(
    long Id,
    string? Name,
    string? Description,
    string? UsuarioModificacion
);
