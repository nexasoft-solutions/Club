using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Queries.GetConceptTypePayrolls;

public sealed record GetConceptTypePayrollsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ConceptTypePayrollResponse>>;
