namespace NexaSoft.Club.Api.Controllers.HumanResources.AttendanceRecords.Request;

public sealed record DeleteAttendanceRecordRequest(
   long Id,
   string DeletedBy
);
