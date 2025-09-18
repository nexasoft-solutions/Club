using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Api.Controllers.Masters.Users.Requests;

public sealed record AssignRolesRequest
(
    long UserId,
    List<RoleDefultResponse> RoleDefs
);
