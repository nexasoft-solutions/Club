using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.Currencies;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Queries.GetCurrency;

public sealed record GetCurrencyQuery(
    long Id
) : IQuery<CurrencyResponse>;
