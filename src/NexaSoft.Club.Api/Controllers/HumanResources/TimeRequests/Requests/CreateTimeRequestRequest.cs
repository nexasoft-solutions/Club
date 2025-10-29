namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequests.Request;

public sealed record CreateTimeRequestRequest(
    long? EmployeeId,
    long? TimeRequestTypeId,
    DateOnly? StartDate,
    DateOnly? EndDate,
    int TotalDays,
    string Reason,
    long? StatusId,
    string CreatedBy
);
