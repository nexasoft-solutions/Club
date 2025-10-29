namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceRecords.Request;

public sealed record UpdateAttendanceRecordRequest(
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
);
