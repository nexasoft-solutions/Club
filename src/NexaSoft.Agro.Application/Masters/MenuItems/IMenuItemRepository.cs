using NexaSoft.Agro.Domain.Masters.Menus;

namespace NexaSoft.Agro.Application.Masters.MenuItems;

public interface IMenuItemRepository
{
    Task<List<MenuItemResponse>> GetMenuByUser(Guid IdUser, Guid IdRole, CancellationToken cancellationToken);

}
