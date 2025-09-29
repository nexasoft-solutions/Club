namespace NexaSoft.Club.Api.Controllers.Masters.Users.Requests;

public record class RevokeRolesRequest
(
    long UserId,
    List<long> RoleIds
);