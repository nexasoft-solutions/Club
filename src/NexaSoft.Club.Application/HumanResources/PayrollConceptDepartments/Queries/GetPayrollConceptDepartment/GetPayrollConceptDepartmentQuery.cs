using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Queries.GetPayrollConceptDepartment;

public sealed record GetPayrollConceptDepartmentQuery(
    long Id
) : IQuery<PayrollConceptDepartmentResponse>;
