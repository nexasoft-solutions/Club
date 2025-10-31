using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.PayrollTypes.Events;

public sealed record PayrollTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
