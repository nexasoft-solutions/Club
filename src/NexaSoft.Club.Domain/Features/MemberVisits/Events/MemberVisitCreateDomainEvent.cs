using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.MemberVisits.Events;

public sealed record MemberVisitCreateDomainEvent
(
    long Id
): IDomainEvent;
