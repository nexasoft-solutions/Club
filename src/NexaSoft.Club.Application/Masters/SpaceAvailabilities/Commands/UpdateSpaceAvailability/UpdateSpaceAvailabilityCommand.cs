using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.UpdateSpaceAvailability;

public sealed record UpdateSpaceAvailabilityCommand(
    long Id,
    long SpaceId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string UpdatedBy
) : ICommand<bool>;
