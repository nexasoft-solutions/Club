namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollStatusTypes.Request;

public sealed record CreatePayrollStatusTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
