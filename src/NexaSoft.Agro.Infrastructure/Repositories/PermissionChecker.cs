using Microsoft.EntityFrameworkCore;
using NexaSoft.Agro.Application.Masters.Permissions;
using NexaSoft.Agro.Domain.Masters.Permissions;
using NexaSoft.Agro.Domain.Masters.Roles;
using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class PermissionChecker(ApplicationDbContext _dbContext) : IPermissionChecker
{
    public async Task<bool> HasPermissionForRoleAsync(Guid userId, string roleName, string permissionName)
    {
        var result = await (
            from ur in _dbContext.Set<UserRole>()
            join r in _dbContext.Set<Role>() on ur.RoleId equals r.Id
            join rp in _dbContext.Set<RolePermission>() on r.Id equals rp.RoleId
            join p in _dbContext.Set<Permission>() on rp.PermissionId equals p.Id
            where ur.UserId == userId && r.Name == roleName && p.Name == permissionName
            select p.Id
        ).AnyAsync();

        return result;
    }
}
