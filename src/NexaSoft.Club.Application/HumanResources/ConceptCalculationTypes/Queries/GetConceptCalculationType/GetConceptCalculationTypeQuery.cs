using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes;

namespace NexaSoft.Club.Application.HumanResources.ConceptCalculationTypes.Queries.GetConceptCalculationType;

public sealed record GetConceptCalculationTypeQuery(
    long Id
) : IQuery<ConceptCalculationTypeResponse>;
