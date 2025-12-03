using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Roles;
using NexaSoft.Club.Domain.Specifications;

namespace NexaSoft.Club.Application.Masters.Roles.Queries.GetRoles;

public sealed record GetRolesQuery(
    BaseSpecParams SpecParams
): IQuery<Pagination<RoleResponse>>;


