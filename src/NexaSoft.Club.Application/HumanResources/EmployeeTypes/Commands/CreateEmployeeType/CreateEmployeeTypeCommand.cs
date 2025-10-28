using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.CreateEmployeeType;

public sealed record CreateEmployeeTypeCommand(
    string? Code,
    string? Name,
    string? Description,
    decimal BaseSalary,
    string CreatedBy
) : ICommand<long>;
