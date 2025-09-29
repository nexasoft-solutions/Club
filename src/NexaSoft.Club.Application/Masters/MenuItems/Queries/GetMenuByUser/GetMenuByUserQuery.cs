using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuByUser;

public sealed record GetMenuByUserQuery(long IdUser, long IdRole) : IQuery<List<MenuItemResponse>>;

