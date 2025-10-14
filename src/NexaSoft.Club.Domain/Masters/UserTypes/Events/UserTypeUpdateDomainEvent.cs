using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.UserTypes.Events;

public sealed record UserTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
