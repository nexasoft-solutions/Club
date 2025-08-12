namespace NexaSoft.Agro.Application.Masters.Permissions;

public interface IPermissionChecker
{
    Task<bool> HasPermissionForRoleAsync(Guid userId, string roleName, string permissionNAme);
}
