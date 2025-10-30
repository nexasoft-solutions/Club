using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.UpdatePayrollConceptDepartment;

public sealed record UpdatePayrollConceptDepartmentCommand(
    long Id,
    long? PayrollConceptId,
    long? DepartmentId,
    string UpdatedBy
) : ICommand<bool>;
