namespace NexaSoft.Agro.Api.Controllers.Masters.Roles.Request;

public sealed record AssignPermissionsRequest
(
    long RoleId,
    List<long> PermissionIds
);