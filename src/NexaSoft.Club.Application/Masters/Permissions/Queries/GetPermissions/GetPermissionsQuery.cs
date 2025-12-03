using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Permissions;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Permissions.Queries.GetPermissions;

public sealed record GetPermissionsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PermissionResponse>>;
