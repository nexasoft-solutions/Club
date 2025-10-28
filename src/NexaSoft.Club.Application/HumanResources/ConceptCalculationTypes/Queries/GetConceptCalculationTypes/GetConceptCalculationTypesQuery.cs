using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Queries.GetConceptCalculationTypes;

public sealed record GetConceptCalculationTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ConceptCalculationTypeResponse>>;
