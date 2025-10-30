namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

public sealed record PayrollConceptDepartmentResponse(
    long Id,
    long? PayrollConceptId,
    string? PayrollConceptCode,
    long? DepartmentId,
    string? DepartmentCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
