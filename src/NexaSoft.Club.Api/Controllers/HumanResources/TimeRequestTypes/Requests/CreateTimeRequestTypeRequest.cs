namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequestTypes.Request;

public sealed record CreateTimeRequestTypeRequest(
    string? Code,
    string? Name,
    bool DeductsSalary,
    bool RequiresApproval,
    string? Description,
    string CreatedBy
);
