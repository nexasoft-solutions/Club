using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.Users.Events;

public sealed record UserCreateDomainEvent
(
    long Id
): IDomainEvent;
