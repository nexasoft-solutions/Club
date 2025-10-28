namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollStatusTypes.Request;

public sealed record UpdatePayrollStatusTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    string UpdatedBy
);
