namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollTypes.Request;

public sealed record UpdatePayrollTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
