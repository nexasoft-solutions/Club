using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.EntryItems.Events;

public sealed record EntryItemUpdateDomainEvent
(
    long Id
): IDomainEvent;
