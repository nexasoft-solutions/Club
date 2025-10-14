using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.AccountTypes;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Queries.GetAccountTypes;

public sealed record GetAccountTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<AccountTypeResponse>>;
