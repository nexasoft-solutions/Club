using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.DocumentTypes.Events;

public sealed record DocumentTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
