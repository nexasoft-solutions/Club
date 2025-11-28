namespace NexaSoft.Club.Api.Controllers.Masters.Roles.Request;

public sealed record CreateRoleRequest
(
    string? Name,
    string? Description,
    string? CreatedBy
);
