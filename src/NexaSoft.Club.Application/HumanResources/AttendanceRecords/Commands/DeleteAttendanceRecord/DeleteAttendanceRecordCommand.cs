using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.DeleteAttendanceRecord;

public sealed record DeleteAttendanceRecordCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
