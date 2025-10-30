using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.CreatePayrollConceptDepartment;

public sealed record CreatePayrollConceptDepartmentCommand(
    long? PayrollConceptId,
    long? DepartmentId,
    string CreatedBy
) : ICommand<long>;
