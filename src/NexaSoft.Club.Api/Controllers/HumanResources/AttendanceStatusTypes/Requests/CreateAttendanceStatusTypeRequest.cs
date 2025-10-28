namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceStatusTypes.Request;

public sealed record CreateAttendanceStatusTypeRequest(
    string? Code,
    string? Name,
    bool? IsPaid,
    string? Description,
    string CreatedBy
);
