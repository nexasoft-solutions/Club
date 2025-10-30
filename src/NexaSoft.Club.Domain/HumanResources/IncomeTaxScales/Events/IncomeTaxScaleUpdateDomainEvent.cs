using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.IncomeTaxScales.Events;

public sealed record IncomeTaxScaleUpdateDomainEvent
(
    long Id
): IDomainEvent;
