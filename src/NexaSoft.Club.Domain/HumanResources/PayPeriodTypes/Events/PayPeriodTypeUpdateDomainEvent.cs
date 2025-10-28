using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayPeriodTypes.Events;

public sealed record PayPeriodTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
