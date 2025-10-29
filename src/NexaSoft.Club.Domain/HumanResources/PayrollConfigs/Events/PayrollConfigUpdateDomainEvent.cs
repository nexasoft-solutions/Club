using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollConfigs.Events;

public sealed record PayrollConfigUpdateDomainEvent
(
    long Id
): IDomainEvent;
