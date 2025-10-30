using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollConceptEmployeeTypes;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Queries.GetPayrollConceptEmployeeTypes;

public sealed record GetPayrollConceptEmployeeTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollConceptEmployeeTypeResponse>>;
