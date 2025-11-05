using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmploymentContracts.Commands.UpdateEmploymentContract;

public sealed record UpdateEmploymentContractCommand(
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
) : ICommand<bool>;
