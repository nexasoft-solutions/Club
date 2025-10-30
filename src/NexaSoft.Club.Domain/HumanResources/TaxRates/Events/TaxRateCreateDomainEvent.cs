using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TaxRates.Events;

public sealed record TaxRateCreateDomainEvent
(
    long Id
): IDomainEvent;
