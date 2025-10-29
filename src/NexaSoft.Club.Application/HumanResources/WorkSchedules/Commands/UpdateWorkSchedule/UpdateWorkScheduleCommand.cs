using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.UpdateWorkSchedule;

public sealed record UpdateWorkScheduleCommand(
    long Id,
    long? EmployeeId,
    int DayOfWeek,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string UpdatedBy
) : ICommand<bool>;
