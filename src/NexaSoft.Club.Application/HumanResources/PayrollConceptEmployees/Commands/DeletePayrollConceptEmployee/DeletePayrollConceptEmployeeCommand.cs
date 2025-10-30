using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.DeletePayrollConceptEmployee;

public sealed record DeletePayrollConceptEmployeeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
