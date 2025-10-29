using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceRecords.Events;

public sealed record AttendanceRecordUpdateDomainEvent
(
    long Id
): IDomainEvent;
