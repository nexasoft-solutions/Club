using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Menus;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenus;

public sealed record GetMenusQuery(
    BaseSpecParams SpecParams
):IQuery<Pagination<MenuResponse>>;
