using NexaSoft.Agro.Domain.Masters.Users;

namespace NexaSoft.Agro.Api.Controllers.Masters.Users.Requests;

public sealed record AssignRolesRequest
(
    Guid UserId,
    List<RoleDefultResponse> RoleDefs
);
