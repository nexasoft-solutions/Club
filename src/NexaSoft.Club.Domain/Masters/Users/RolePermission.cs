

namespace NexaSoft.Club.Domain.Masters.Users;

public class RolePermission
{
    public long RoleId { get; private set; }
    public long PermissionId { get; private set; }

    // Constructor privado para EF Core
    private RolePermission() { }

    public RolePermission(long roleId, long permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}
