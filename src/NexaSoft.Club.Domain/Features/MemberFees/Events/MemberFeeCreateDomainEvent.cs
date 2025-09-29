using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.MemberFees.Events;

public sealed record MemberFeeCreateDomainEvent
(
    long Id
): IDomainEvent;
