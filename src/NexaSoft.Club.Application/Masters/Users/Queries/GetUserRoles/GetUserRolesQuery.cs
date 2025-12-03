using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Roles;

namespace NexaSoft.Club.Application.Masters.Users.Queries.GetUserRoles;

public sealed record GetUserRolesQuery
(
    long UserId
) : IQuery<List<UserRoleResponse>>;


