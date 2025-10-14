namespace NexaSoft.Club.Domain.Masters.Statuses;

public sealed record StatusResponse(
    long Id,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
