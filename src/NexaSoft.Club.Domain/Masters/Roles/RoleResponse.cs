namespace NexaSoft.Club.Domain.Masters.Roles;

public sealed record RoleResponse
(
    long Id,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);



public sealed record PermissionBasicResponse
(
    long Id, string? Name
);