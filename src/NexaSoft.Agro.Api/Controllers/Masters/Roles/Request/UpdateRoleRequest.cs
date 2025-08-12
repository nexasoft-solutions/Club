namespace NexaSoft.Agro.Api.Controllers.Masters.Roles.Request;

public record class UpdateRoleRequest
(
    Guid Id,
    string? Name,
    string? Description
);
