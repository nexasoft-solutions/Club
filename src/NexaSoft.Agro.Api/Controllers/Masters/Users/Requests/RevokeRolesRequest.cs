namespace NexaSoft.Agro.Api.Controllers.Masters.Users.Requests;

public record class RevokeRolesRequest
(
    long UserId,
    List<long> RoleIds
);