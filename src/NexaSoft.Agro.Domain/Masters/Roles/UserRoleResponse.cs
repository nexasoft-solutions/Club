namespace NexaSoft.Agro.Domain.Masters.Roles;

public sealed record UserRoleResponse
(
    long Id,
    string? Name,
    bool IsDefault
);