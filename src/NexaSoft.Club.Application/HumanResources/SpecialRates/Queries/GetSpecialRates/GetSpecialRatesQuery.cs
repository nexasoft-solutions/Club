using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Queries.GetSpecialRates;

public sealed record GetSpecialRatesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SpecialRateResponse>>;
