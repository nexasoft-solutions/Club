using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmployeesInfo.Commands.DeleteEmployeeInfo;

public sealed record DeleteEmployeeInfoCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
