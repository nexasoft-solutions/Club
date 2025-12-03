namespace NexaSoft.Club.Domain.Masters.Permissions;

public record class PermissionResponse
(
    long Id,
    string? Name,
    string? Description,
    string? Reference,
    string? Source,
    string? Action,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy

);