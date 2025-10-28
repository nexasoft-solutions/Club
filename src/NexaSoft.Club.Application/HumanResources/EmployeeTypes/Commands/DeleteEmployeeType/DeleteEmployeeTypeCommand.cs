using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.EmployeeTypes.Commands.DeleteEmployeeType;

public sealed record DeleteEmployeeTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
