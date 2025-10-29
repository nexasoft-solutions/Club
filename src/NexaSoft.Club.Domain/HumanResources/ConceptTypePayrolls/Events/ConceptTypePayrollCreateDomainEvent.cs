using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.ConceptTypePayrolls.Events;

public sealed record ConceptTypePayrollCreateDomainEvent
(
    long Id
): IDomainEvent;
