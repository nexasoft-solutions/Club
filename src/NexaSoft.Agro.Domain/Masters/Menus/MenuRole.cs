namespace NexaSoft.Agro.Domain.Masters.Menus;

public class MenuRole
{
    public Guid MenuItemOptionId { get; private set; }
    public Guid RoleId { get; private set; }

    public MenuRole(Guid menuItemOptionId, Guid roleId)
    {
        MenuItemOptionId = menuItemOptionId;
        RoleId = roleId;
    }
}
