namespace NexaSoft.Club.Domain.Masters.SystemUsers;

public sealed record SystemUserResponse(
    long Id,
    string? Username,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Role,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
