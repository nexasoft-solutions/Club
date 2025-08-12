using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Menus;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Queries.GetMenuByUser;

public sealed record GetMenuByUserQuery(Guid IdUser, Guid IdRole) : IQuery<List<MenuItemResponse>>;

