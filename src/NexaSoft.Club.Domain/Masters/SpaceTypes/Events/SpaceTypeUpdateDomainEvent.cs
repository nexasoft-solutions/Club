using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpaceTypes.Events;

public sealed record SpaceTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
