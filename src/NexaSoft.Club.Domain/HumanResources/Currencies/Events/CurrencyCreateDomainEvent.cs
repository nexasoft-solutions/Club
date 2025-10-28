using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Currencies.Events;

public sealed record CurrencyCreateDomainEvent
(
    long Id
): IDomainEvent;
