using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members.Events;

public sealed record MemberQrGenerationRequiredDomainEvent
(
    long MemberId,
    string MemberName,
    string Dni,
    DateOnly JoinDate,
    DateOnly ExpirationDate,
    string CreatedBy,
    DateTime CreatedOn
): IDomainEvent;
