namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeeTypes.Request;

public sealed record UpdateEmployeeTypeRequest(
   long Id,
    string? Code,
    string? Name,
    string? Description,
    decimal BaseSalary,
    string UpdatedBy
);
