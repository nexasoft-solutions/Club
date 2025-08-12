using NexaSoft.Agro.Application.Abstractions.Messaging;
using NexaSoft.Agro.Domain.Masters.Roles;

namespace NexaSoft.Agro.Application.Masters.Permissions.Queries.GetRolesPermissions;

public sealed record GetRolesPermissionsQuery: IQuery<List<RolesPermissionsResponse>>;
