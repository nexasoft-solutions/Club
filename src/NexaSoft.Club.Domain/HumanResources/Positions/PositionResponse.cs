namespace NexaSoft.Club.Domain.HumanResources.Positions;

public sealed record PositionResponse(
    long Id,
    string? Code,
    string? Name,
    long? DepartmentId,
    string? DepartmentCode,
    long? EmployeeTypeId,
    string? EmployeeCode,
    decimal BaseSalary,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
