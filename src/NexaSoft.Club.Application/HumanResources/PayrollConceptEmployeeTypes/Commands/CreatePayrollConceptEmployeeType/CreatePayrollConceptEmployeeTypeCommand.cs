using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.CreatePayrollConceptEmployeeType;

public sealed record CreatePayrollConceptEmployeeTypeCommand(
    long? PayrollConceptId,
    long? EmployeeTypeId,
    string CreatedBy
) : ICommand<long>;
