using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriodStatuses.Events;

public sealed record PayrollPeriodStatusUpdateDomainEvent
(
    long Id
): IDomainEvent;
