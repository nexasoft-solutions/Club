using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Positions.Events;

public sealed record PositionUpdateDomainEvent
(
    long Id
): IDomainEvent;
