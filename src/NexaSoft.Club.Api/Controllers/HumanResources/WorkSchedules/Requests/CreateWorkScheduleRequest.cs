namespace NexaSoft.Club.Api.Controllers.HumanResources.WorkSchedules.Request;

public sealed record CreateWorkScheduleRequest(
    long? EmployeeId,
    int DayOfWeek,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string CreatedBy
);
