using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.WorkSchedules.Events;

public sealed record WorkScheduleCreateDomainEvent
(
    long Id
): IDomainEvent;
