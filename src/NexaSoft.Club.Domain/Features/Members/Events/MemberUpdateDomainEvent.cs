using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members.Events;

public sealed record MemberUpdateDomainEvent
(
    long Id
): IDomainEvent;
