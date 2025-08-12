using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Permissions;

namespace NexaSoft.Agro.Application.Masters.Permissions.Queries.GetPermissions;

public sealed record GetPermissionsQuery: IQuery<List<PermissionResponse>>;

