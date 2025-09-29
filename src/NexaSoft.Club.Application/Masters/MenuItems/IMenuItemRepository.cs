using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems;

public interface IMenuItemRepository
{
    Task<List<MenuItemResponse>> GetMenuByUser(long IdUser, long IdRole, CancellationToken cancellationToken);

}
