using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Queries.GetAttendanceRecord;

public sealed record GetAttendanceRecordQuery(
    long Id
) : IQuery<AttendanceRecordResponse>;
