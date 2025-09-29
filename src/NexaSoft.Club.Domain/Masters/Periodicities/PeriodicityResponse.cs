namespace NexaSoft.Club.Domain.Masters.Periodicities;

public sealed record PeriodicityResponse(
    long Id,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
