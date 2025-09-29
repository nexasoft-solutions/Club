namespace NexaSoft.Club.Domain.Masters.Roles;

public sealed record UserRoleResponse
(
    long Id,
    string? Name,
    bool IsDefault
);