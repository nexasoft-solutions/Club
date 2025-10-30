using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.UpdatePayrollConceptEmployeeType;

public sealed record UpdatePayrollConceptEmployeeTypeCommand(
    long Id,
    long? PayrollConceptId,
    long? EmployeeTypeId,
    string UpdatedBy
) : ICommand<bool>;
