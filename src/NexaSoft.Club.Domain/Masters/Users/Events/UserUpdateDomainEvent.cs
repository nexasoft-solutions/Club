using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Users.Events;

public sealed record UserUpdateDomainEvent
(
    long Id
): IDomainEvent;
