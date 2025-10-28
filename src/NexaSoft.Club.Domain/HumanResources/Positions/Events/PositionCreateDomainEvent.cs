using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Positions.Events;

public sealed record PositionCreateDomainEvent
(
    long Id
): IDomainEvent;
