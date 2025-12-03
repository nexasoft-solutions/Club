using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.MenuItems.Queries.GetMenuRoles;

public sealed record GetMenuRolesQuery(long MenuId) : IQuery<List<long>>;
