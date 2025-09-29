namespace NexaSoft.Club.Api.Controllers.Masters.Permissions.Request;

public sealed record CreatePermissionRequest
(
    string? Name,
    string? Description,
    string? ReferenciaControl,
    string? UsuarioCreacion
);
