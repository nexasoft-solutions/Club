using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes.Events;

public sealed record AttendanceStatusTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
