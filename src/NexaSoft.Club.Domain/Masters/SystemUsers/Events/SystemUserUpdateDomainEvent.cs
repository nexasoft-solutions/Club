using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SystemUsers.Events;

public sealed record SystemUserUpdateDomainEvent
(
    long Id
): IDomainEvent;
