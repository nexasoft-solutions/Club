using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Queries.GetBankAccountTypes;

public sealed record GetBankAccountTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<BankAccountTypeResponse>>;
