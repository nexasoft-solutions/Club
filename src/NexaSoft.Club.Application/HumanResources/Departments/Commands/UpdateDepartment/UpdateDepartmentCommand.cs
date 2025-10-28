using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Departments.Commands.UpdateDepartment;

public sealed record UpdateDepartmentCommand(
    long Id,
    string? Code,
    string? Name,
    long? ParentDepartmentId,
    string? Description,
    long? ManagerId,
    long? CostCenterId,
    string? Location,
    string? PhoneExtension,
    string UpdatedBy
) : ICommand<bool>;
