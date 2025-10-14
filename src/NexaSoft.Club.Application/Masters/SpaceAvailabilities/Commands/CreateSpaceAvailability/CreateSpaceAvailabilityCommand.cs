using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Commands.CreateSpaceAvailability;

public sealed record CreateSpaceAvailabilityCommand(
    long SpaceId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,  
    string CreatedBy
) : ICommand<long>;
