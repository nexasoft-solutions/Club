using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Periodicities.Events;

public sealed record PeriodicityUpdateDomainEvent
(
    long Id
): IDomainEvent;
