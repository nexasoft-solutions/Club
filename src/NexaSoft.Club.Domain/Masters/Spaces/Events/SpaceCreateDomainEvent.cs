using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Spaces.Events;

public sealed record SpaceCreateDomainEvent
(
    long Id
): IDomainEvent;
