using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetRoles;

public sealed record GetRolesQuery: IQuery<List<RoleResponse>>;
