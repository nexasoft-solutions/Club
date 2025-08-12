namespace NexaSoft.Agro.Api.Controllers.Masters.Users.Requests;

public record class RevokeRolesRequest
(
    Guid UserId,
    List<Guid> RoleIds
);