namespace NexaSoft.Club.Domain.HumanResources.EmployeeTypes;

public sealed record EmployeeTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    decimal BaseSalary,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
