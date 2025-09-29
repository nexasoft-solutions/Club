using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SystemUsers.Events;

public sealed record SystemUserCreateDomainEvent
(
    long Id
): IDomainEvent;
