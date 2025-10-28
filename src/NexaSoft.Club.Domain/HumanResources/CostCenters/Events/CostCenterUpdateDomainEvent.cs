using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CostCenters.Events;

public sealed record CostCenterUpdateDomainEvent
(
    long Id
): IDomainEvent;
