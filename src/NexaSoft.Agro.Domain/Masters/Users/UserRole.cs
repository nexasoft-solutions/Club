namespace NexaSoft.Agro.Domain.Masters.Users;

public class UserRole
{

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public bool IsDefault { get; private set; }

    private UserRole() { }

    public UserRole(Guid userId, Guid roleId, bool isDefault)
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
