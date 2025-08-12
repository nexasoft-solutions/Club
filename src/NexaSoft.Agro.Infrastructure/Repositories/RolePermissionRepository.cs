using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Permissions;
using NexaSoft.Agro.Domain.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class RolePermissionRepository(ApplicationDbContext _dbContext) : IRolePermissionRepository
{
    public async Task AddAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<RolePermission>().AddAsync(new RolePermission(roleId, permissionId), cancellationToken);
    }

    public async Task<int> AddRangeAsync(Guid roleId, IEnumerable<Guid> permissionIds, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.Set<RolePermission>()
           .Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
           .Select(rp => rp.PermissionId)
           .ToListAsync(cancellationToken);

        var toAdd = permissionIds.Except(existing)
            .Select(permissionId => new RolePermission(roleId, permissionId));

        await _dbContext.Set<RolePermission>().AddRangeAsync(toAdd, cancellationToken);
        return toAdd.Count();
    }

    public async Task ClearForRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        var relations = await _dbContext.Set<RolePermission>()
            .Where(rp => rp.RoleId == roleId)
            .ToListAsync(cancellationToken);

        if (relations.Any())
        {
            _dbContext.Set<RolePermission>().RemoveRange(relations);
        }
    }

    public async Task<int> CountPermissionsForRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<RolePermission>()
            .CountAsync(rp => rp.RoleId == roleId, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<RolePermission>()
           .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);
    }

    public async Task<List<string?>> GetPermissionNamesForRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<RolePermission>()
            .Where(rp => rp.RoleId == roleId)
            .Join(_dbContext.Set<Permission>(),
                rp => rp.PermissionId,
                p => p.Id,
                (rp, p) => p.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Guid>> GetPermissionsForRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<RolePermission>()
           .Where(rp => rp.RoleId == roleId)
           .Select(rp => rp.PermissionId)
           .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<Guid, List<Guid>>> GetPermissionsForRolesAsync(IEnumerable<Guid> roleIds, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<RolePermission>()
           .Where(rp => roleIds.Contains(rp.RoleId))
           .GroupBy(rp => rp.RoleId)
           .ToDictionaryAsync(
               g => g.Key,
               g => g.Select(rp => rp.PermissionId).ToList(),
               cancellationToken);
    }

    public async Task<List<RolesPermissionsResponse>> GetRolesPermissionsAsync(CancellationToken cancellationToken)
    {
        var result = await (
            from r in _dbContext.Set<Role>()
            join rp in _dbContext.Set<RolePermission>() on r.Id equals rp.RoleId
            join p in _dbContext.Set<Permission>() on rp.PermissionId equals p.Id
            where r.FechaEliminacion == null && p.FechaEliminacion == null
            select new RolesPermissionsResponse
            (
                rp.RoleId,
                r.Name,
                rp.PermissionId,
                p.Name,
                p.ReferenciaControl
            )
        ).ToListAsync(cancellationToken);

        // ordena en memoria
        var ordered = result
            .OrderBy(x => x.NombreRol)
            .ThenBy(x => x.ReferenciaControl)
            .ThenBy(x => x.NombrePermiso)
            .ToList();

        return ordered;
    }

    public async Task<bool> RemoveAsync(Guid roleId, Guid permissionId, CancellationToken cancellationToken = default)
    {
        var relation = await _dbContext.Set<RolePermission>()
           .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, cancellationToken);

        if (relation != null)
        {
            _dbContext.Set<RolePermission>().Remove(relation);
            return true;
        }
        return false;
    }

    public async Task<int> RemoveRangeAsync(Guid roleId, IEnumerable<Guid> permissionIds, CancellationToken cancellationToken = default)
    {
        var relations = await _dbContext.Set<RolePermission>()
            .Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
            .ToListAsync(cancellationToken);

        if (relations.Any())
        {
            _dbContext.Set<RolePermission>().RemoveRange(relations);
        }
        return relations.Count;
    }

    public async Task<bool> RoleHasPermissionAsync(Guid roleId, string permissionName, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<RolePermission>()
            .Where(rp => rp.RoleId == roleId)
            .Join(_dbContext.Set<Permission>(),
                rp => rp.PermissionId,
                p => p.Id,
                (rp, p) => p)
            .AnyAsync(p => p.Name == permissionName, cancellationToken);
    }
}
