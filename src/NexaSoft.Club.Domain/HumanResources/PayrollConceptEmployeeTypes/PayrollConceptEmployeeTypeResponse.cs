namespace NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

public sealed record PayrollConceptEmployeeTypeResponse(
    long Id,
    long? PayrollConceptId,
    string? PayrollConceptCode,
    long? EmployeeTypeId,
    string? EmployeeTypeCode,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
