using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CostCenters.Events;

public sealed record CostCenterCreateDomainEvent
(
    long Id
): IDomainEvent;
