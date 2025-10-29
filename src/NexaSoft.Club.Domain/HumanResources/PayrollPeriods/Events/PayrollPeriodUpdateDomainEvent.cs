using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollPeriods.Events;

public sealed record PayrollPeriodUpdateDomainEvent
(
    long Id
): IDomainEvent;
