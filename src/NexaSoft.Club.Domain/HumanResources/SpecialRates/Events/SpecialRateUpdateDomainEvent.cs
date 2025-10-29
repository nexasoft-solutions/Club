using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.SpecialRates.Events;

public sealed record SpecialRateUpdateDomainEvent
(
    long Id
): IDomainEvent;
