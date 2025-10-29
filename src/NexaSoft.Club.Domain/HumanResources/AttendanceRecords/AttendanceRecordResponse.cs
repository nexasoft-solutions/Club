namespace NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

public sealed record AttendanceRecordResponse(
    long Id,
    long? EmployeeId,
    string? EmployeeCode,
    long? CostCenterId,
    string? CostCenterCode,
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
    string? AttendanceStatusTypeCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
