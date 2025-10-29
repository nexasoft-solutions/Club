using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.WorkSchedules.Events;

public sealed record WorkScheduleUpdateDomainEvent
(
    long Id
): IDomainEvent;
