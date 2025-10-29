using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Commands.DeleteCompanyBankAccount;

public sealed record DeleteCompanyBankAccountCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
