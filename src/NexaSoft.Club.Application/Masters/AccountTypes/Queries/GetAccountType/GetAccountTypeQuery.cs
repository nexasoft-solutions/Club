using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.AccountTypes;

namespace NexaSoft.Club.Application.Masters.AccountTypes.Queries.GetAccountType;

public sealed record GetAccountTypeQuery(
    long Id
) : IQuery<AccountTypeResponse>;
