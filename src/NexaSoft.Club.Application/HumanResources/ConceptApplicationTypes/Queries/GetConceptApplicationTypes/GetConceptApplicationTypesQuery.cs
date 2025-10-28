using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Queries.GetConceptApplicationTypes;

public sealed record GetConceptApplicationTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ConceptApplicationTypeResponse>>;
