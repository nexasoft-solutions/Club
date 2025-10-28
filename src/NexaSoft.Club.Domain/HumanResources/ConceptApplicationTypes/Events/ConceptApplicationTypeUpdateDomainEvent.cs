using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ConceptApplicationTypes.Events;

public sealed record ConceptApplicationTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
