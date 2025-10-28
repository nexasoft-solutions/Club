using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Banks.Events;

public sealed record BankCreateDomainEvent
(
    long Id
): IDomainEvent;
