using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.LegalParameters.Events;

public sealed record LegalParameterCreateDomainEvent
(
    long Id
): IDomainEvent;
