namespace NexaSoft.Club.Api.Controllers.Masters.Roles.Request;

public record class UpdateRoleRequest
(
    long Id,
    string? Name,
    string? Description,
    string? UpdatedBy
);
