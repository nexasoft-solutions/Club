using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.CreatePayrollConceptEmployee;

public sealed record CreatePayrollConceptEmployeeCommand(
    long? PayrollConceptId,
    long? EmployeeId,
    string CreatedBy
) : ICommand<long>;
