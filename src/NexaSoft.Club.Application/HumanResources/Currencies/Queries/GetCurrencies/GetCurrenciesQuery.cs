using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.Currencies;

namespace NexaSoft.Club.Application.HumanResources.Currencies.Queries.GetCurrencies;

public sealed record GetCurrenciesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CurrencyResponse>>;
