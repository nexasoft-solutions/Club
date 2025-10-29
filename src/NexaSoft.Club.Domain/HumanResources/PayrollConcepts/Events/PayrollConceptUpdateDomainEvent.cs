using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts.Events;

public sealed record PayrollConceptUpdateDomainEvent
(
    long Id
): IDomainEvent;
