using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Banks.Events;

public sealed record BankUpdateDomainEvent
(
    long Id
): IDomainEvent;
