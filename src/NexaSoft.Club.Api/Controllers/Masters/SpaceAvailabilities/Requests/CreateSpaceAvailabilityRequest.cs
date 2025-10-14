namespace NexaSoft.Club.Api.Controllers.Masters.SpaceAvailabilities.Request;

public sealed record CreateSpaceAvailabilityRequest(
    long SpaceId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string CreatedBy
);
