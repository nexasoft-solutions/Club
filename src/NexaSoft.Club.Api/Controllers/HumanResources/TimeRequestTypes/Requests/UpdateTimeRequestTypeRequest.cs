namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequestTypes.Request;

public sealed record UpdateTimeRequestTypeRequest(
   long Id,
    string? Code,
    string? Name,
    bool DeductsSalary,
    bool RequiresApproval,
    string? Description,
    string UpdatedBy
);
