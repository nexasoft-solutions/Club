using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Domain.Specifications;

public class MenuWithRolesSpec: BaseSpecification<MenuItemOption>
{
    public MenuWithRolesSpec(long menuId)
        : base(m => m.Id == menuId)
    {
        AddInclude(m => m.Roles);
    }
}
