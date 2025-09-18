
namespace NexaSoft.Agro.Application.Masters.Users.Queries.GetUserRolesAndPermissions;


public sealed record UserRolesPermissionsResponse(
    string NombreRol,
    string ReferenciaControl,
    string Permiso
);

/*public sealed record UserRolesPermissionsResponse
(
    long UserId,
    List<RoleDto> Roles,
    List<string> AllPermissions
);

public record RoleDto(
    long Id,
    string Name,
    List<string> Permissions
);*/

/*public sealed record UserRolesPermissionsDto
{
    public long UserId { get; init; }
    public string? UserName { get; init; }
    public IEnumerable<UserRoleDto?>? Roles { get; init; }
    public IEnumerable<string?>? AllPermissions { get; init; }
}

public sealed record UserRoleDto
{
    public long RoleId { get; init; }
    public string? RoleName { get; init; }
    public IEnumerable<RolePermissionDto?>? Permissions { get; init; }
}

public sealed record RolePermissionDto
{
    public long PermissionId { get; init; }
    public string? PermissionName { get; init; }
}*/

