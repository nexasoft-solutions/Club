using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.AccountingEntries.Events;

public sealed record AccountingEntryUpdateDomainEvent
(
    long Id
): IDomainEvent;
