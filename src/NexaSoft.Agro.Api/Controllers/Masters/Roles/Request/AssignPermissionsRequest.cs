namespace NexaSoft.Agro.Api.Controllers.Masters.Roles.Request;

public sealed record AssignPermissionsRequest
(
    Guid RoleId,
    List<Guid> PermissionIds
);