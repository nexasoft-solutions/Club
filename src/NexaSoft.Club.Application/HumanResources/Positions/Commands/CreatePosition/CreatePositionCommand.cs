using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Positions.Commands.CreatePosition;

public sealed record CreatePositionCommand(
    string? Code,
    string? Name,
    long? DepartmentId,
    long? EmployeeTypeId,
    decimal BaseSalary,
    string? Description,
    string CreatedBy
) : ICommand<long>;
