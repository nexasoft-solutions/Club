using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.AttendanceStatusTypes.Commands.UpdateAttendanceStatusType;

public sealed record UpdateAttendanceStatusTypeCommand(
    long Id,
    string? Code,
    string? Name,
    bool? IsPaid,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
