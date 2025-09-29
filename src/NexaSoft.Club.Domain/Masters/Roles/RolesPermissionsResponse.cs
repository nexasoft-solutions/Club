namespace NexaSoft.Club.Domain.Masters.Roles;

public sealed record RolesPermissionsResponse
(
    long RoleId,
    string? NombreRol,
    long PermissionId,
    string? NombrePermiso,
    string? ReferenciaControl
);