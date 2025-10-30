using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.TaxRates;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Queries.GetTaxRates;

public sealed record GetTaxRatesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<TaxRateResponse>>;
