using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.EntryItems.Events;

public sealed record EntryItemCreateDomainEvent
(
    long Id
): IDomainEvent;
