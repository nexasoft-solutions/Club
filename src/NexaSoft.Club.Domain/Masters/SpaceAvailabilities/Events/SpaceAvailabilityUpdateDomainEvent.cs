using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpaceAvailabilities.Events;

public sealed record SpaceAvailabilityUpdateDomainEvent
(
    long Id
): IDomainEvent;
