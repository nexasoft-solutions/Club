namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollTypes.Request;

public sealed record CreatePayrollTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    string CreatedBy
);
