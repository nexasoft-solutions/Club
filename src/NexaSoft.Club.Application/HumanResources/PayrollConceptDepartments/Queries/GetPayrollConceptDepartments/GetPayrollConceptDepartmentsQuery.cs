using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptDepartments;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Queries.GetPayrollConceptDepartments;

public sealed record GetPayrollConceptDepartmentsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollConceptDepartmentResponse>>;
