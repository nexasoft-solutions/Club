namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceStatusTypes.Request;

public sealed record DeleteAttendanceStatusTypeRequest(
   long Id,
   string DeletedBy
);
