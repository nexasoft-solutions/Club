using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.UpdateEmployeeType;

public sealed record UpdateEmployeeTypeCommand(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    decimal BaseSalary,
    string UpdatedBy
) : ICommand<bool>;
