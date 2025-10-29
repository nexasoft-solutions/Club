using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.TimeRequests.Events;

public sealed record TimeRequestCreateDomainEvent
(
    long Id
): IDomainEvent;
