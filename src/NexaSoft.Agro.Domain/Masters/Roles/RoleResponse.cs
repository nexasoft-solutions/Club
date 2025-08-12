namespace NexaSoft.Agro.Domain.Masters.Roles;

public sealed record RoleResponse
(
    Guid Id,
    string? Name,
    string? Description
);
