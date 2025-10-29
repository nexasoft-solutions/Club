namespace NexaSoft.Club.Api.Controllers.HumanResources.WorkSchedules.Request;

public sealed record UpdateWorkScheduleRequest(
   long Id,
    long? EmployeeId,
    int DayOfWeek,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string UpdatedBy
);
