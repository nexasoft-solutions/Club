using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpaceRates.Events;

public sealed record SpaceRateUpdateDomainEvent
(
    long Id
): IDomainEvent;
