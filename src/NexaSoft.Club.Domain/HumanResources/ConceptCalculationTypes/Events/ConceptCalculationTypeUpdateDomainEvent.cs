using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ConceptCalculationTypes.Events;

public sealed record ConceptCalculationTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
