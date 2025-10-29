using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.CreateWorkSchedule;

public sealed record CreateWorkScheduleCommand(
    long? EmployeeId,
    int DayOfWeek,
    TimeOnly? StartTime,
    TimeOnly? EndTime,
    string CreatedBy
) : ICommand<long>;
