using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.CreateAttendanceRecord;

public sealed record CreateAttendanceRecordCommand(
    long? EmployeeId,
    long? CostCenterId,
    DateOnly RecordDate,
    TimeOnly? CheckInTime,
    TimeOnly? CheckOutTime,
    decimal? TotalHours,
    decimal? RegularHours,
    decimal? OvertimeHours,
    decimal? SundayHours,
    decimal? HolidayHours,
    decimal? NightHours,
    int? LateMinutes,
    int? EarlyDepartureMinutes,
    long? AttendanceStatusTypeId,
    string CreatedBy
) : ICommand<long>;
