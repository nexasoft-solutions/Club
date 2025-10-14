namespace NexaSoft.Club.Domain.Masters.SpaceTypes;

public sealed record SpaceTypeResponse(
    long Id,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
