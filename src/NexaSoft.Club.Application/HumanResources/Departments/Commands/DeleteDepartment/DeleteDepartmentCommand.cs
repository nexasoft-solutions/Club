using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Departments.Commands.DeleteDepartment;

public sealed record DeleteDepartmentCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
