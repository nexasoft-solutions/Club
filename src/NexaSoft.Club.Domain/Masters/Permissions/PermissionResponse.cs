namespace NexaSoft.Club.Domain.Masters.Permissions;

public record class PermissionResponse
(
    long Id,
    string? Name,
    string? Description,
    string? ReferenciaControl,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy

);