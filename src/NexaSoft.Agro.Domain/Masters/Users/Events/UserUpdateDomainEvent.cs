using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Masters.Users.Events;

public sealed record UserUpdateDomainEvent
(
    long Id
): IDomainEvent;
