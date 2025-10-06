using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.MemberVisits.Events;

public sealed record MemberVisitUpdateDomainEvent
(
    long Id
): IDomainEvent;
