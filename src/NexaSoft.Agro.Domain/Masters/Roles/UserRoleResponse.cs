namespace NexaSoft.Agro.Domain.Masters.Roles;

public sealed record UserRoleResponse
(
    Guid Id,
    string? Name,
    bool IsDefault
);