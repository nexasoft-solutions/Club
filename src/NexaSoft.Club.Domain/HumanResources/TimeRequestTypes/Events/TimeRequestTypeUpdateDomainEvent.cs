using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TimeRequestTypes.Events;

public sealed record TimeRequestTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
