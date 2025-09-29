using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.MemberTypes.Events;

public sealed record MemberTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
