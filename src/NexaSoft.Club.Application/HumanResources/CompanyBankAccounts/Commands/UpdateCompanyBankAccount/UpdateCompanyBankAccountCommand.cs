using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.UpdateCompanyBankAccount;

public sealed record UpdateCompanyBankAccountCommand(
    long Id,
    long? CompanyId,
    long? BankId,
    long? BankAccountTypeId,
    string? BankAccountNumber,
    string? CciNumber,
    long? CurrencyId,
    bool IsPrimary,
    bool IsActive,
    string UpdatedBy
) : ICommand<bool>;
