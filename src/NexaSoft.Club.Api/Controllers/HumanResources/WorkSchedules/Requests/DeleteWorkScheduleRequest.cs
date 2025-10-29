namespace NexaSoft.Club.Api.Controllers.HumanResources.WorkSchedules.Request;

public sealed record DeleteWorkScheduleRequest(
   long Id,
   string DeletedBy
);
