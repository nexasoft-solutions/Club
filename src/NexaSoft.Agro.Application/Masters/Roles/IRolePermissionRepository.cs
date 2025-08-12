
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Roles;

public interface IRolePermissionRepository
{
    // Operaciones básicas
    Task AddAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default);

    // Operaciones masivas
    Task<int> AddRangeAsync(Guid roleId, IEnumerable<Guid> permissionIds, CancellationToken cancellationToken = default);
    Task<int> RemoveRangeAsync(Guid roleId, IEnumerable<Guid> permissionIds, CancellationToken cancellationToken = default);
    Task ClearForRoleAsync(Guid roleId, CancellationToken cancellationToken = default);

    // Consultas
    Task<int> CountPermissionsForRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<List<Guid>> GetPermissionsForRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<List<string?>> GetPermissionNamesForRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<bool> RoleHasPermissionAsync(Guid roleId, string permissionName, CancellationToken cancellationToken = default);

    // Consultas avanzadas
    Task<Dictionary<Guid, List<Guid>>> GetPermissionsForRolesAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken = default);

    Task<List<RolesPermissionsResponse>> GetRolesPermissionsAsync(CancellationToken cancellationToken);
}
