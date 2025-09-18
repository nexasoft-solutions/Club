
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Roles;

public interface IRolePermissionRepository
{
    // Operaciones básicas
    Task AddAsync(long roleId, long permissionId, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(long roleId, long permissionId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(long roleId, long permissionId, CancellationToken cancellationToken = default);

    // Operaciones masivas
    Task<int> AddRangeAsync(long roleId, IEnumerable<long> permissionIds, CancellationToken cancellationToken = default);
    Task<int> RemoveRangeAsync(long roleId, IEnumerable<long> permissionIds, CancellationToken cancellationToken = default);
    Task ClearForRoleAsync(long roleId, CancellationToken cancellationToken = default);

    // Consultas
    Task<int> CountPermissionsForRoleAsync(long roleId, CancellationToken cancellationToken = default);
    Task<List<long>> GetPermissionsForRoleAsync(long roleId, CancellationToken cancellationToken = default);
    Task<List<string?>> GetPermissionNamesForRoleAsync(long roleId, CancellationToken cancellationToken = default);
    Task<bool> RoleHasPermissionAsync(long roleId, string permissionName, CancellationToken cancellationToken = default);

    // Consultas avanzadas
    Task<Dictionary<long, List<long>>> GetPermissionsForRolesAsync(IEnumerable<long> roleIds, CancellationToken cancellationToken = default);

    Task<List<RolesPermissionsResponse>> GetRolesPermissionsAsync(CancellationToken cancellationToken);
}
