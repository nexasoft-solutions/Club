using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Queries.GetPayrollConcepts;

public sealed record GetPayrollConceptsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PayrollConceptResponse>>;
