using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Roles.Queries.GetRoles;

public sealed record GetRolesQuery: IQuery<List<RoleResponse>>;
