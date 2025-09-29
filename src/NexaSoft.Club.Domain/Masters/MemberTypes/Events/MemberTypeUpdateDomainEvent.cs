using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.MemberTypes.Events;

public sealed record MemberTypeUpdateDomainEvent
(
    long Id
): IDomainEvent;
