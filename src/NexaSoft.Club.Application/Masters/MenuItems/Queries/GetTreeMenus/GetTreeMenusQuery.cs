using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetTreeMenus;

public sealed record GetTreeMenusQuery
 :IQuery<List<MenuResponse>>;