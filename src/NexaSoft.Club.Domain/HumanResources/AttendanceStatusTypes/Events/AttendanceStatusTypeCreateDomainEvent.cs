using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes.Events;

public sealed record AttendanceStatusTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
