namespace NexaSoft.Club.Domain.HumanResources.WorkSchedules;

public sealed record WorkScheduleResponse(
    long Id,
    long? EmployeeId,
    string? EmployeeCode,
    int DayOfWeek,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
