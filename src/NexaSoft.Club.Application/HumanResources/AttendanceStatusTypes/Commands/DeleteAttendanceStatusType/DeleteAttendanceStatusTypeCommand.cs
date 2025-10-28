using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.DeleteAttendanceStatusType;

public sealed record DeleteAttendanceStatusTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
