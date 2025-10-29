using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

namespace NexaSoft.Club.Application.HumanResources.CompanyBankAccounts.Queries.GetCompanyBankAccounts;

public sealed record GetCompanyBankAccountsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CompanyBankAccountResponse>>;
