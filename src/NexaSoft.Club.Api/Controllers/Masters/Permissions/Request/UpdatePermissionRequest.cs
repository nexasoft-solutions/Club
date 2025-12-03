namespace NexaSoft.Club.Api.Controllers.Masters.Permissions.Request;

public sealed record UpdatePermissionRequest
(
    long Id,
    string? Name,
    string? Description,
    string? Reference,
    string? Source,
    string? Action,
    string? UpdatedBy
);