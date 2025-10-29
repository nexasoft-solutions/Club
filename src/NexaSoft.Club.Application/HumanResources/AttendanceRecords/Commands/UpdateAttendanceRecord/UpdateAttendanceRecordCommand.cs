using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.AttendanceRecords.Commands.UpdateAttendanceRecord;

public sealed record UpdateAttendanceRecordCommand(
    long Id,
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
    string UpdatedBy
) : ICommand<bool>;
