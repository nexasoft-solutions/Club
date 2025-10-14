using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Statuses.Events;

public sealed record StatusCreateDomainEvent
(
    long Id
): IDomainEvent;
