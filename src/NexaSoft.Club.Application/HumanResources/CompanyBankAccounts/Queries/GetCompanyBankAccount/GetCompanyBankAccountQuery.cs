using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Queries.GetCompanyBankAccount;

public sealed record GetCompanyBankAccountQuery(
    long Id
) : IQuery<CompanyBankAccountResponse>;
