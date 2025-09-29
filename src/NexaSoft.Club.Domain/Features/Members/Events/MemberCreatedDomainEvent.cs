using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.Members.Events;

public sealed record MemberCreatedDomainEvent
(
    long MemberId,
    long MemberTypeId,
    DateOnly JoinDate, 
    DateOnly? ExpirationDate,
    string CreatedBy
) : IDomainEvent;
