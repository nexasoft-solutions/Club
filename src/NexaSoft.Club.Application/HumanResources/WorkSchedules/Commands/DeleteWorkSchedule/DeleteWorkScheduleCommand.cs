using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.WorkSchedules.Commands.DeleteWorkSchedule;

public sealed record DeleteWorkScheduleCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
