using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.Banks;

namespace NexaSoft.Club.Application.HumanResources.Banks.Queries.GetBanks;

public sealed record GetBanksQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<BankResponse>>;
