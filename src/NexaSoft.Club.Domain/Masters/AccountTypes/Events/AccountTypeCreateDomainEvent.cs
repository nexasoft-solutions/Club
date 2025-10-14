using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.AccountTypes.Events;

public sealed record AccountTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
