namespace NexaSoft.Club.Domain.HumanResources.Departments;

public sealed record DepartmentResponse(
    long Id,
    string? Code,
    string? Name,
    long? ParentDepartmentId,
    string? ParentDepartmentCode,
    string? Description,
    long? ManagerId,
    string? EmployeeCode,
    long? CostCenterId,
    string? CostCenterCode,
    string? Location,
    string? PhoneExtension,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
