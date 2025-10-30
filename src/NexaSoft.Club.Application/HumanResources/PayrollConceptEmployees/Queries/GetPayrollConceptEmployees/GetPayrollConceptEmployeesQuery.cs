using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployees;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Queries.GetPayrollConceptEmployees;

public sealed record GetPayrollConceptEmployeesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollConceptEmployeeResponse>>;
