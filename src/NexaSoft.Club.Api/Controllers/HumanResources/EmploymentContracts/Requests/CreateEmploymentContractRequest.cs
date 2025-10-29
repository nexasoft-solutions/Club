namespace NexaSoft.Club.Api.Controllers.HumanResources.EmploymentContracts.Request;

public sealed record CreateEmploymentContractRequest(
    long? EmployeeId,
    long? ContractTypeId,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Salary,
    int WorkingHours,
    string? DocumentPath,
    string CreatedBy
);
