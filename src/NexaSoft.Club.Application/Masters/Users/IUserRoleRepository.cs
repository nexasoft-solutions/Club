using NexaSoft.Club.Application.Masters.Users.Queries.GetUserRolesAndPermissions;
using NexaSoft.Club.Domain.Masters.Roles;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Application.Masters.Users;

public interface IUserRoleRepository
{
    // Operaciones básicas
    Task AddAsync(long userId, long roleId, bool isDefault, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(long userId, long roleId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(long userId, long roleId, CancellationToken cancellationToken = default);

    // Operaciones masivas
    Task<int> AddRangeAsync(long userId, IEnumerable<RoleDefultResponse> roleDefs, CancellationToken cancellationToken = default);
    Task<int> RemoveRangeAsync(long userId, IEnumerable<long> roleIds, CancellationToken cancellationToken = default);
    Task ClearForUserAsync(long userId, CancellationToken cancellationToken = default);
    Task ClearForRoleAsync(long roleId, CancellationToken cancellationToken = default);

    // Consultas
    Task<List<long>> GetRolesForUserAsync(long userId, CancellationToken cancellationToken = default);
    Task<List<long>> GetUsersForRoleAsync(long roleId, CancellationToken cancellationToken = default);
    Task<int> CountRolesForUserAsync(long userId, CancellationToken cancellationToken = default);

    // Consultas avanzadas
    Task<Dictionary<long, List<long>>> GetRolesForUsersAsync(IEnumerable<long> userIds, CancellationToken cancellationToken = default);
    Task<List<UserRolesPermissionsResponse>> GetUserRolesPermissions(long UserId, CancellationToken cancellationToken);

    Task<List<string>> GetUserPermissionsAsync(long userId, CancellationToken cancellationToken);

    Task<List<UserRoleResponse>> GetUserRolesAsync(long userId, CancellationToken cancellationToken = default);

    Task<List<string>> GetPermissionsForDefaultRoleAsync(long userId, long RoleId, CancellationToken cancellationToken = default);

}
