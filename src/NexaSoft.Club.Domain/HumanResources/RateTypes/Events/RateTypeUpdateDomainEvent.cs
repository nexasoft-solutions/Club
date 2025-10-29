using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.RateTypes.Events;

public sealed record RateTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
