namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequests.Request;

public sealed record UpdateTimeRequestRequest(
   long Id,
    long? EmployeeId,
    long? TimeRequestTypeId,
    DateOnly? StartDate,
    DateOnly? EndDate,
    int TotalDays,
    string Reason,
    long? StatusId,
    string UpdatedBy
);
