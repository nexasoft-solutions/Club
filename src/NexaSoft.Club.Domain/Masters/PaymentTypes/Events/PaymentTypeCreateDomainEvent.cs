using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.PaymentTypes.Events;

public sealed record PaymentTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
