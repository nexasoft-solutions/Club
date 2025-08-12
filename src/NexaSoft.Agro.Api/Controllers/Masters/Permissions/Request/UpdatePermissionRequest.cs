namespace NexaSoft.Agro.Api.Controllers.Masters.Permissions.Request;

public sealed record UpdatePermissionRequest
(
    Guid Id,
    string? Name,
    string? Description,
    string? ReferenciaControl
);