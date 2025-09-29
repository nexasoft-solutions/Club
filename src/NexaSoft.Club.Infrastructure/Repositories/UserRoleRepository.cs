using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NexaSoft.Club.Application.Abstractions.Data;
using NexaSoft.Club.Application.Masters.Users;
using NexaSoft.Club.Application.Masters.Users.Queries.GetUserRolesAndPermissions;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Masters.Roles;
using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext _dbContext, ISqlConnectionFactory _sqlConnectionFactory) : IUserRoleRepository
{

    public async Task AddAsync(long userId, long roleId, bool isDefault, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<UserRole>().AddAsync(new UserRole(userId, roleId, isDefault), cancellationToken);
    }

    public async Task<int> AddRangeAsync(long userId, IEnumerable<RoleDefultResponse> roleDefs, CancellationToken cancellationToken = default)
    {
        var existingRoles = await _dbContext.Set<UserRole>()
         .Where(ur => ur.UserId == userId)
         .ToListAsync(cancellationToken);

        var roleIdsFromInput = roleDefs.Select(rd => rd.RoleId).ToHashSet();

        // Separar nuevos roles y actualizaciones
        var rolesToAdd = roleDefs
            .Where(rd => !existingRoles.Any(er => er.RoleId == rd.RoleId))
            .Select(rd => new UserRole(userId, rd.RoleId, rd.IsDefault ?? false))
            .ToList();

        var rolesToUpdate = existingRoles
            .Where(er => roleIdsFromInput.Contains(er.RoleId))
            .ToList();

        // Actualizar IsDefault de roles existentes si ha cambiado
        foreach (var existing in rolesToUpdate)
        {
            var incoming = roleDefs.First(rd => rd.RoleId == existing.RoleId);
            if (incoming.IsDefault.HasValue && existing.IsDefault != incoming.IsDefault.Value)
            {
                existing.SetDefault(incoming.IsDefault.Value);
            }
        }

        // Garantizar que solo un rol tenga IsDefault = true
        var defaultCount = roleDefs.Count(r => r.IsDefault == true);
        if (defaultCount > 1)
        {
            throw new InvalidOperationException("Solo un rol puede ser marcado como predeterminado.");
        }

        // Si hay un rol marcado como predeterminado, marcar los demás como false
        if (defaultCount == 1)
        {
            var defaultRoleId = roleDefs.First(r => r.IsDefault == true).RoleId;
            foreach (var role in existingRoles.Where(r => r.RoleId != defaultRoleId))
            {
                role.SetDefault(false);
            }
        }

        await _dbContext.Set<UserRole>().AddRangeAsync(rolesToAdd, cancellationToken);
        return rolesToAdd.Count + rolesToUpdate.Count;
    }

    public async Task<bool> RemoveAsync(long userId, long roleId, CancellationToken cancellationToken = default)
    {
        var relation = await _dbContext.Set<UserRole>()
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);

        if (relation != null)
        {
            _dbContext.Set<UserRole>().Remove(relation);
            return true;
        }
        return false;
    }

    public async Task<int> RemoveRangeAsync(long userId, IEnumerable<long> roleIds, CancellationToken cancellationToken = default)
    {
        var relations = await _dbContext.Set<UserRole>()
            .Where(ur => ur.UserId == userId && roleIds.Contains(ur.RoleId))
            .ToListAsync(cancellationToken);

        if (relations.Any())
        {
            _dbContext.Set<UserRole>().RemoveRange(relations);
        }
        return relations.Count;
    }

    public async Task ClearForUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        var relations = await _dbContext.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .ToListAsync(cancellationToken);

        if (relations.Any())
        {
            _dbContext.Set<UserRole>().RemoveRange(relations);
        }
    }

    public async Task ClearForRoleAsync(long roleId, CancellationToken cancellationToken = default)
    {
        var relations = await _dbContext.Set<UserRole>()
            .Where(ur => ur.RoleId == roleId)
            .ToListAsync(cancellationToken);

        if (relations.Any())
        {
            _dbContext.Set<UserRole>().RemoveRange(relations);
        }
    }

    public async Task<bool> ExistsAsync(long userId, long roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserRole>()
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
    }

    public async Task<List<long>> GetRolesForUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<long>> GetUsersForRoleAsync(long roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserRole>()
            .Where(ur => ur.RoleId == roleId)
            .Select(ur => ur.UserId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountRolesForUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserRole>()
            .CountAsync(ur => ur.UserId == userId, cancellationToken);
    }

    public async Task<Dictionary<long, List<long>>> GetRolesForUsersAsync(IEnumerable<long> userIds, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserRole>()
            .Where(ur => userIds.Contains(ur.UserId))
            .GroupBy(ur => ur.UserId)
            .ToDictionaryAsync(
                g => g.Key,
                g => g.Select(ur => ur.RoleId).ToList(),
                cancellationToken);
    }

    public async Task<List<UserRolesPermissionsResponse>> GetUserRolesPermissions(long UserId, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = "SELECT * FROM get_user_permissions_by_userid(@id);";
        var result = await connection.QueryAsync<UserRolesPermissionsResponse>(
         new CommandDefinition(sql, new { id = UserId }, cancellationToken: cancellationToken));

        return result.ToList();

    }

    public async Task<List<string>> GetUserPermissionsAsync(long userId, CancellationToken cancellationToken)
    {
        var result = await (
            from ur in _dbContext.Set<UserRole>()
            join r in _dbContext.Set<Role>() on ur.RoleId equals r.Id
            join rp in _dbContext.Set<RolePermission>() on r.Id equals rp.RoleId
            join p in _dbContext.Set<Permission>() on rp.PermissionId equals p.Id
            where ur.UserId == userId
            select p.Name
        ).Distinct().ToListAsync(cancellationToken);

        return result;
    }

    public async Task<List<UserRoleResponse>> GetUserRolesAsync(long userId, CancellationToken cancellationToken = default)
    {
        var roles = await (
            from ur in _dbContext.Set<UserRole>()
            join r in _dbContext.Set<Role>() on ur.RoleId equals r.Id
            where ur.UserId == userId
            select new UserRoleResponse(
                r.Id,
                r.Name!,
                ur.IsDefault
            )
        ).ToListAsync(cancellationToken);

        return roles;
    }

    public async Task<List<string>> GetPermissionsForDefaultRoleAsync(long userId, long RoleId, CancellationToken cancellationToken = default)
    {
        var permissions = await _dbContext.Set<RolePermission>()
       .Where(rp => rp.RoleId == RoleId)
       .Join(
           _dbContext.Set<Permission>(),
           rp => rp.PermissionId,
           p => p.Id,
           (rp, p) => p.Name
       )
       .ToListAsync(cancellationToken);

        if (permissions.IsNullOrEmpty())
            throw new InvalidOperationException("El usuario no tiene permisos par el rol asignado.");


        return permissions!;
    }
}
