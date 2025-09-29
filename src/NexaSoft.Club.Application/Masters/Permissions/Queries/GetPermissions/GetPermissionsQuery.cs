using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Permissions;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetPermissions;

public sealed record GetPermissionsQuery: IQuery<List<PermissionResponse>>;

