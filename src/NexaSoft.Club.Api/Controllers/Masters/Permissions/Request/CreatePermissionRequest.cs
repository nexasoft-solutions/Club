namespace NexaSoft.Club.Api.Controllers.Masters.Permissions.Request;

public sealed record CreatePermissionRequest
(
    string? Name,
    string? Description,
    string? Reference,
    string? Source,
    string? Action,
    string? CreatedBy
);
