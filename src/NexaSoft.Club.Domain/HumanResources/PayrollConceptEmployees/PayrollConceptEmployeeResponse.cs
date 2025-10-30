namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

public sealed record PayrollConceptEmployeeResponse(
    long Id,
    long? PayrollConceptId,
    string? PayrollConceptCode,
    long? EmployeeId,
    string? EmployeeCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
