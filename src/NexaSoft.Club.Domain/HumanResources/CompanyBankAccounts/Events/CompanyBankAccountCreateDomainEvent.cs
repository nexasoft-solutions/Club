using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts.Events;

public sealed record CompanyBankAccountCreateDomainEvent
(
    long Id
): IDomainEvent;
