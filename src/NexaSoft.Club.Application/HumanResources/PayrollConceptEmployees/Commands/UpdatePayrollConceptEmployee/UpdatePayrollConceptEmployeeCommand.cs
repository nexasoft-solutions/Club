using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.UpdatePayrollConceptEmployee;

public sealed record UpdatePayrollConceptEmployeeCommand(
    long Id,
    long? PayrollConceptId,
    long? EmployeeId,
    string UpdatedBy
) : ICommand<bool>;
