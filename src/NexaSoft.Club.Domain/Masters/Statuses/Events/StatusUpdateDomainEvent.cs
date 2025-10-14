using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Statuses.Events;

public sealed record StatusUpdateDomainEvent
(
    long Id
): IDomainEvent;
