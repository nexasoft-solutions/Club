using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.UserTypes.Events;

public sealed record UserTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
