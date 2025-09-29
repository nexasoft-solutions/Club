using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.MemberFees.Events;

public sealed record MemberFeeUpdateDomainEvent
(
    long Id
): IDomainEvent;
