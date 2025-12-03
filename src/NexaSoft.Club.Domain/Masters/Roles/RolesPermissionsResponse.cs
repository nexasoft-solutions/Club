namespace NexaSoft.Club.Domain.Masters.Roles;

public sealed record RolesPermissionsResponse
(
    long RoleId,
    string? NameRol,
    long PermissionId,
    string? NamePermission,
    string? Reference,
    string? Source,
    string? Action
);