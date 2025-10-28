using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Currencies.Events;

public sealed record CurrencyUpdateDomainEvent
(
    long Id
): IDomainEvent;
