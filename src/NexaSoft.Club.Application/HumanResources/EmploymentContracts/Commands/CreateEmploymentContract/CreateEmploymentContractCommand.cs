using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.CreateEmploymentContract;

public sealed record CreateEmploymentContractCommand(
    long? EmployeeId,
    long? ContractTypeId,
    DateOnly StartDate,
    DateOnly? EndDate,
    decimal Salary,
    int WorkingHours,
    string? DocumentPath,
    bool? IsActive,
    string CreatedBy
) : ICommand<long>;
