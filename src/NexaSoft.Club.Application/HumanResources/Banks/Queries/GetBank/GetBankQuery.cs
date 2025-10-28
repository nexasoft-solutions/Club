using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.Banks;

namespace NexaSoft.Club.Application.HumanResources.Banks.Queries.GetBank;

public sealed record GetBankQuery(
    long Id
) : IQuery<BankResponse>;
