using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.DeletePayrollConceptDepartment;

public sealed record DeletePayrollConceptDepartmentCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
