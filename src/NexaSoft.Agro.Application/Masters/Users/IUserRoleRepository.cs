using NexaSoft.Agro.Application.Masters.Users.Queries.GetUserRolesAndPermissions;
using NexaSoft.Agro.Domain.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Application.Masters.Users;

public interface IUserRoleRepository
{
    // Operaciones básicas
    Task AddAsync(Guid userId, Guid roleId, bool isDefault, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);

    // Operaciones masivas
    Task<int> AddRangeAsync(Guid userId, IEnumerable<RoleDefultResponse> roleDefs, CancellationToken cancellationToken = default);
    Task<int> RemoveRangeAsync(Guid userId, IEnumerable<Guid> roleIds, CancellationToken cancellationToken = default);
    Task ClearForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task ClearForRoleAsync(Guid roleId, CancellationToken cancellationToken = default);

    // Consultas
    Task<List<Guid>> GetRolesForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<Guid>> GetUsersForRoleAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<int> CountRolesForUserAsync(Guid userId, CancellationToken cancellationToken = default);

    // Consultas avanzadas
    Task<Dictionary<Guid, List<Guid>>> GetRolesForUsersAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default);
    Task<List<UserRolesPermissionsResponse>> GetUserRolesPermissions(Guid UserId, CancellationToken cancellationToken);

    Task<List<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);

    Task<List<UserRoleResponse>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<List<string>> GetPermissionsForDefaultRoleAsync(Guid userId, Guid RoleId, CancellationToken cancellationToken = default);

}
