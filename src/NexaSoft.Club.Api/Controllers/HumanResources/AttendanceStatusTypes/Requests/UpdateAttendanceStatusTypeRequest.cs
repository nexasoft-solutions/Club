namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceStatusTypes.Request;

public sealed record UpdateAttendanceStatusTypeRequest(
   long Id,
    string? Code,
    string? Name,
    bool? IsPaid,
    string? Description,
    string UpdatedBy
);
