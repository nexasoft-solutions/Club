namespace NexaSoft.Club.Domain.Masters.UserTypes;

public sealed record UserTypeResponse(
    long Id,
    string? Name,
    string? Description,
    bool IsAdministrative,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
