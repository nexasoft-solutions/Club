using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetRolesPermissions;

public sealed record GetRolesPermissionsQuery
(
    long RoleId
) : IQuery<List<PermissionBasicResponse>>;