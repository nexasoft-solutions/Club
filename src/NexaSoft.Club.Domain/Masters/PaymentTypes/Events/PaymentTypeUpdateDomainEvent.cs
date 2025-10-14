using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.PaymentTypes.Events;

public sealed record PaymentTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
