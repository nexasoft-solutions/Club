using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.MemberStatuses.Events;

public sealed record MemberStatusUpdateDomainEvent
(
    long Id
): IDomainEvent;
