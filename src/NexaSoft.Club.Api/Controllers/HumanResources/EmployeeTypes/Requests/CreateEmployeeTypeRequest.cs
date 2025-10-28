namespace NexaSoft.Club.Api.Controllers.HumanResources.EmployeeTypes.Request;

public sealed record CreateEmployeeTypeRequest(
    string? Code,
    string? Name,
    string? Description,
    decimal BaseSalary,
    string CreatedBy
);
