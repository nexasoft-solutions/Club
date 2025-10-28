using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CostCenterTypes.Events;

public sealed record CostCenterTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
