using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.MemberStatuses.Events;

public sealed record MemberStatusCreateDomainEvent
(
    long Id
): IDomainEvent;
