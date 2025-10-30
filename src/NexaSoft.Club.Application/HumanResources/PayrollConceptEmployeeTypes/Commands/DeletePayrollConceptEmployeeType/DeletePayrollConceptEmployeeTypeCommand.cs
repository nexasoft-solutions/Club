using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.DeletePayrollConceptEmployeeType;

public sealed record DeletePayrollConceptEmployeeTypeCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
