namespace NexaSoft.Club.Api.Controllers.HumanResources.Positions.Request;

public sealed record UpdatePositionRequest(
   long Id,
    string? Code,
    string? Name,
    long? DepartmentId,
    long? EmployeeTypeId,
    decimal BaseSalary,
    string? Description,
    string UpdatedBy
);
