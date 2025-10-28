using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Departments.Commands.CreateDepartment;

public sealed record CreateDepartmentCommand(
    string? Code,
    string? Name,
    long? ParentDepartmentId,
    string? Description,
    long? ManagerId,
    long? CostCenterId,
    string? Location,
    string? PhoneExtension,
    string CreatedBy
) : ICommand<long>;
