using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts.Events;

public sealed record CompanyBankAccountUpdateDomainEvent
(
    long Id
): IDomainEvent;
