using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Positions.Commands.UpdatePosition;

public sealed record UpdatePositionCommand(
    long Id,
    string? Code,
    string? Name,
    long? DepartmentId,
    long? EmployeeTypeId,
    decimal BaseSalary,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
