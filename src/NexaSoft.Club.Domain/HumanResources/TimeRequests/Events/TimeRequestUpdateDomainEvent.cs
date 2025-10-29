using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TimeRequests.Events;

public sealed record TimeRequestUpdateDomainEvent
(
    long Id
): IDomainEvent;
