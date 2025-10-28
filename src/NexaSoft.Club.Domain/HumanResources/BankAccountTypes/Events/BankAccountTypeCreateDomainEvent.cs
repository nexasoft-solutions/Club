using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.BankAccountTypes.Events;

public sealed record BankAccountTypeCreateDomainEvent
(
    long Id
): IDomainEvent;
