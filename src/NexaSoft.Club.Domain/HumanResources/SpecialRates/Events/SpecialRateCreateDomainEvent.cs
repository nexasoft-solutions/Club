using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.SpecialRates.Events;

public sealed record SpecialRateCreateDomainEvent
(
    long Id
): IDomainEvent;
