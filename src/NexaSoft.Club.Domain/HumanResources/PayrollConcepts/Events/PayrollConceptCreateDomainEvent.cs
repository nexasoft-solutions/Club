using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts.Events;

public sealed record PayrollConceptCreateDomainEvent
(
    long Id
): IDomainEvent;
