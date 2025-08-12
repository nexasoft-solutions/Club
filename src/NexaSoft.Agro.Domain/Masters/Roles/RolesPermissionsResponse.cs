namespace NexaSoft.Agro.Domain.Masters.Roles;

public sealed record RolesPermissionsResponse
(
    Guid RoleId,
    string? NombreRol,
    Guid PermissionId,
    string? NombrePermiso,
    string? ReferenciaControl
);