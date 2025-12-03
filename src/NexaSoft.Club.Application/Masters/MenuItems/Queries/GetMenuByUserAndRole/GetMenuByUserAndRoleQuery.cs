using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuByUserAndRole;

public sealed record GetMenuByUserAndRoleQuery
(
    long UserId,
    long RoleId
): IQuery<List<MenuItemsResponse>>;
