using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;
using NexaSoft.Club.Domain.HumanResources.CostCenters;
using NexaSoft.Club.Domain.HumanResources.AttendanceStatusTypes;

namespace NexaSoft.Club.Domain.HumanResources.AttendanceRecords;

public class AttendanceRecord : Entity
{
    public long? EmployeeId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }
    public DateOnly RecordDate { get; private set; }
    public TimeOnly? CheckInTime { get; private set; }
    public TimeOnly? CheckOutTime { get; private set; }
    public decimal? TotalHours { get; private set; }
    public decimal? RegularHours { get; private set; }
    public decimal? OvertimeHours { get; private set; }
    public decimal? SundayHours { get; private set; }
    public decimal? HolidayHours { get; private set; }
    public decimal? NightHours { get; private set; }
    public int? LateMinutes { get; private set; }
    public int? EarlyDepartureMinutes { get; private set; }
    public long? AttendanceStatusTypeId { get; private set; }
    public AttendanceStatusType? AttendanceStatusType { get; private set; }
    public int StateAttendanceRecord { get; private set; }

    private AttendanceRecord() { }

    private AttendanceRecord(
        long? employeeId, 
        long? costCenterId, 
        DateOnly recordDate, 
        TimeOnly? checkInTime, 
        TimeOnly? checkOutTime, 
        decimal? totalHours, 
        decimal? regularHours, 
        decimal? overtimeHours, 
        decimal? sundayHours, 
        decimal? holidayHours, 
        decimal? nightHours, 
        int? lateMinutes, 
        int? earlyDepartureMinutes, 
        long? attendanceStatusTypeId, 
        int stateAttendanceRecord, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        EmployeeId = employeeId;
        CostCenterId = costCenterId;
        RecordDate = recordDate;
        CheckInTime = checkInTime;
        CheckOutTime = checkOutTime;
        TotalHours = totalHours;
        RegularHours = regularHours;
        OvertimeHours = overtimeHours;
        SundayHours = sundayHours;
        HolidayHours = holidayHours;
        NightHours = nightHours;
        LateMinutes = lateMinutes;
        EarlyDepartureMinutes = earlyDepartureMinutes;
        AttendanceStatusTypeId = attendanceStatusTypeId;
        StateAttendanceRecord = stateAttendanceRecord;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static AttendanceRecord Create(
        long? employeeId, 
        long? costCenterId, 
        DateOnly recordDate, 
        TimeOnly? checkInTime, 
        TimeOnly? checkOutTime, 
        decimal? totalHours, 
        decimal? regularHours, 
        decimal? overtimeHours, 
        decimal? sundayHours, 
        decimal? holidayHours, 
        decimal? nightHours, 
        int? lateMinutes, 
        int? earlyDepartureMinutes, 
        long? attendanceStatusTypeId, 
        int stateAttendanceRecord, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new AttendanceRecord(
            employeeId,
            costCenterId,
            recordDate,
            checkInTime,
            checkOutTime,
            totalHours,
            regularHours,
            overtimeHours,
            sundayHours,
            holidayHours,
            nightHours,
            lateMinutes,
            earlyDepartureMinutes,
            attendanceStatusTypeId,
            stateAttendanceRecord,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? employeeId, 
        long? costCenterId, 
        DateOnly recordDate, 
        TimeOnly? checkInTime, 
        TimeOnly? checkOutTime, 
        decimal? totalHours, 
        decimal? regularHours, 
        decimal? overtimeHours, 
        decimal? sundayHours, 
        decimal? holidayHours, 
        decimal? nightHours, 
        int? lateMinutes, 
        int? earlyDepartureMinutes, 
        long? attendanceStatusTypeId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        EmployeeId = employeeId;
        CostCenterId = costCenterId;
        RecordDate = recordDate;
        CheckInTime = checkInTime;
        CheckOutTime = checkOutTime;
        TotalHours = totalHours;
        RegularHours = regularHours;
        OvertimeHours = overtimeHours;
        SundayHours = sundayHours;
        HolidayHours = holidayHours;
        NightHours = nightHours;
        LateMinutes = lateMinutes;
        EarlyDepartureMinutes = earlyDepartureMinutes;
        AttendanceStatusTypeId = attendanceStatusTypeId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateAttendanceRecord = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
