namespace NexaSoft.Club.Domain.HumanResources.EmploymentContracts;

public sealed record EmploymentContractResponse(
    long Id,
    long? EmployeeId,
    string? EmployeeCode,
    long? ContractTypeId,
    string? ContractTypeName,
    DateOnly StartDate,
    DateOnly? EndDate,
    decimal Salary,
    int WorkingHours,
    string? DocumentPath,
    bool? IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
