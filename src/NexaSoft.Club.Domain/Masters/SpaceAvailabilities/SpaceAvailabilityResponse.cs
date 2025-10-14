namespace NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

public sealed record SpaceAvailabilityResponse(
    long Id,
    long SpaceId,
    string? SpaceName,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime, 
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
