namespace NexaSoft.Club.Api.Controllers.Masters.SpaceAvailabilities.Request;

public sealed record UpdateSpaceAvailabilityRequest(
   long Id,
    long SpaceId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string UpdatedBy
);
