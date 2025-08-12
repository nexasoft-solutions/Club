using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Menus;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Queries.GetMenus;

public sealed record GetMenusQuery:IQuery<List<MenuResponse>>;
