using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptApplicationTypes.Queries.GetConceptApplicationType;

public sealed record GetConceptApplicationTypeQuery(
    long Id
) : IQuery<ConceptApplicationTypeResponse>;
