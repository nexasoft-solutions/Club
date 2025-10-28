using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

namespace NexaSoft.Club.Application.HumanResources.BankAccountTypes.Queries.GetBankAccountType;

public sealed record GetBankAccountTypeQuery(
    long Id
) : IQuery<BankAccountTypeResponse>;
