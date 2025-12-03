using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Domain.Masters.Menus;

public class MenuRole
{
    public long MenuItemOptionId { get; private set; }
    public long RoleId { get; private set; }

    // Navigation properties (MUY IMPORTANTE)
    public MenuItemOption MenuItemOption { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    public MenuRole(long menuItemOptionId, long roleId)
    {
        MenuItemOptionId = menuItemOptionId;
        RoleId = roleId;
    }

    private MenuRole() { } // Necesario para EF Core
}
