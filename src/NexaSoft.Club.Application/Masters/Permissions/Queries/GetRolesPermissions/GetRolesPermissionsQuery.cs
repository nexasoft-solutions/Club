using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetRolesPermissions;

public sealed record GetRolesPermissionsQuery
(long RoleId): IQuery<List<RolesPermissionsResponse>>;
