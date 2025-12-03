using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems;

public interface IMenuItemRepository
{
    Task<List<MenuItemResponse>> GetMenuByUser(long IdUser, long IdRole, CancellationToken cancellationToken);

    Task<List<long>> GetUserRolesAsync(long MenuId, CancellationToken cancellationToken = default);

    Task<List<MenuItemsResponse>> GetMenuByUserAndRoleAsync(long UserId, long RoleId, CancellationToken cancellationToken = default);

}
