using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.EmployeesInfo;

namespace NexaSoft.Club.Domain.HumanResources.WorkSchedules;

public class WorkSchedule : Entity
{
    public long? EmployeeId { get; private set; }
    public EmployeeInfo? EmployeeInfo { get; private set; }
    public int DayOfWeek { get; private set; }
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly? EndTime { get; private set; }
    public int StateWorkSchedule { get; private set; }

    private WorkSchedule() { }

    private WorkSchedule(
        long? employeeId, 
        int dayOfWeek, 
        TimeOnly? startTime, 
        TimeOnly? endTime, 
        int stateWorkSchedule, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        EmployeeId = employeeId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        StateWorkSchedule = stateWorkSchedule;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static WorkSchedule Create(
        long? employeeId, 
        int dayOfWeek, 
        TimeOnly? startTime, 
        TimeOnly? endTime, 
        int stateWorkSchedule, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new WorkSchedule(
            employeeId,
            dayOfWeek,
            startTime,
            endTime,
            stateWorkSchedule,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? employeeId, 
        int dayOfWeek, 
        TimeOnly? startTime, 
        TimeOnly? endTime, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        EmployeeId = employeeId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateWorkSchedule = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
