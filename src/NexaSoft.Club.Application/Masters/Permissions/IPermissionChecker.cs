namespace NexaSoft.Club.Application.Masters.Permissions;

public interface IPermissionChecker
{
    Task<bool> HasPermissionForRoleAsync(long userId, string roleName, string permissionNAme);
}
