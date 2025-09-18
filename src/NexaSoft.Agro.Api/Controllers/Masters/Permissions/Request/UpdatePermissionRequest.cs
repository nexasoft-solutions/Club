namespace NexaSoft.Agro.Api.Controllers.Masters.Permissions.Request;

public sealed record UpdatePermissionRequest
(
    long Id,
    string? Name,
    string? Description,
    string? ReferenciaControl,
    string? UsuarioModificacion
);