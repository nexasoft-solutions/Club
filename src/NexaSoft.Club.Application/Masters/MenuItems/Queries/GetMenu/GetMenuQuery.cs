using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Menus;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenu;

public sealed record GetMenuQuery
(
    long Id
): IQuery<MenuResponse>;