namespace NexaSoft.Club.Api.Controllers.Masters.Roles.Request;

public sealed record AssignPermissionsRequest
(
    long RoleId,
    List<long> PermissionIds
);