namespace NexaSoft.Club.Api.Controllers.HumanResources.EmploymentContracts.Request;

public sealed record UpdateEmploymentContractRequest(
   long Id,
    long? EmployeeId,
    long? ContractTypeId,
    DateOnly StartDate,
    DateOnly? EndDate,
    decimal Salary,
    int WorkingHours,
    string? DocumentPath,
    bool? IsActive,
    string UpdatedBy
);
