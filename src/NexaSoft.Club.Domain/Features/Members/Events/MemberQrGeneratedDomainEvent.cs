using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members.Events;

public sealed record MemberQrGeneratedDomainEvent
(
    long MemberId,
    string QrCode,
    string QrUrl,
    DateOnly ExpirationDate,
    DateTime GeneratedAt
):IDomainEvent;