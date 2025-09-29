using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.FamilyMembers.Events;

public sealed record FamilyMemberUpdateDomainEvent
(
    long Id
): IDomainEvent;
