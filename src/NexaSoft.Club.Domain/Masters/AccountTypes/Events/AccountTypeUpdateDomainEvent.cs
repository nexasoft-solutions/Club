using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.AccountTypes.Events;

public sealed record AccountTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
