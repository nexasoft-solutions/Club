using NexaSoft.Club.Domain.Masters.Users;

namespace NexaSoft.Club.Api.Controllers.Masters.Users.Requests;

public sealed record AssignRolesRequest
(
    long UserId,
    List<RoleDefultResponse> RoleDefs
);
