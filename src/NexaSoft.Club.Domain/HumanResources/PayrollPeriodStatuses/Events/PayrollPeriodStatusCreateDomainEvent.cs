using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses.Events;

public sealed record PayrollPeriodStatusCreateDomainEvent
(
    long Id
): IDomainEvent;
