using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PaymentMethodTypes.Events;

public sealed record PaymentMethodTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
