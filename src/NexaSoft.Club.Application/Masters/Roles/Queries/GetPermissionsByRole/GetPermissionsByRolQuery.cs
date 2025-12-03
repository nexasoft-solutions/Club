using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetPermissionsByRole;

public sealed record GetPermissionsByRolQuery
(long RoleId): IQuery<List<string>>;
