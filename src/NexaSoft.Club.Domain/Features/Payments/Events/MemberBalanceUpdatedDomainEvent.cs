using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Payments.Events;

public sealed record MemberBalanceUpdatedDomainEvent
(
    long MemberId,
    decimal Amount,
    string UpdatedBy
):IDomainEvent;
