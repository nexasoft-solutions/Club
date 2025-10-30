using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.TaxRates;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Queries.GetTaxRate;

public sealed record GetTaxRateQuery(
    long Id
) : IQuery<TaxRateResponse>;
