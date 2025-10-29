using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.CreateCompanyBankAccount;

public sealed record CreateCompanyBankAccountCommand(
    long? CompanyId,
    long? BankId,
    long? BankAccountTypeId,
    string? BankAccountNumber,
    string? CciNumber,
    long? CurrencyId,
    bool IsPrimary,
    bool IsActive,
    string CreatedBy
) : ICommand<long>;
