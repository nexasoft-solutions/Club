using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Payments.Events;

public sealed record PaymentUpdateDomainEvent
(
    long Id
): IDomainEvent;
