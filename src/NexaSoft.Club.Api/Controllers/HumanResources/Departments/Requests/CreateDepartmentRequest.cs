namespace NexaSoft.Club.Api.Controllers.HumanResources.Departments.Request;

public sealed record CreateDepartmentRequest(
    string? Code,
    string? Name,
    long? ParentDepartmentId,
    string? Description,
    long? ManagerId,
    long? CostCenterId,
    string? Location,
    string? PhoneExtension,
    string CreatedBy
);
