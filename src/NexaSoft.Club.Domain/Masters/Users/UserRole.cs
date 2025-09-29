namespace NexaSoft.Club.Domain.Masters.Users;

public class UserRole
{

    public long UserId { get; private set; }
    public long RoleId { get; private set; }

    public bool IsDefault { get; private set; }

    private UserRole() { }

    public UserRole(long userId, long roleId, bool isDefault)
    {
        UserId = userId;
        RoleId = roleId;
        IsDefault = isDefault;
    }
    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }
}
