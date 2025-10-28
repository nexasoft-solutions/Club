namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequestTypes.Request;

public sealed record UpdateTimeRequestTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
