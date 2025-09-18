namespace NexaSoft.Agro.Domain.Masters.Menus;

public class MenuRole
{
    public long MenuItemOptionId { get; private set; }
    public long RoleId { get; private set; }

    public MenuRole(long menuItemOptionId, long roleId)
    {
        MenuItemOptionId = menuItemOptionId;
        RoleId = roleId;
    }
}
