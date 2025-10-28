using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.CreateAttendanceStatusType;

public sealed record CreateAttendanceStatusTypeCommand(
    string? Code,
    string? Name,
    bool? IsPaid,
    string? Description,
    string CreatedBy
) : ICommand<long>;
