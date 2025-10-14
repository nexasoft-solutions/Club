namespace NexaSoft.Club.Domain.Masters.DocumentTypes;

public sealed record DocumentTypeResponse(
    long Id,
    string? Name,
    string? Description,
    string? Serie,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
