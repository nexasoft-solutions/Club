using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRates;

public sealed record GetSpaceRatesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SpaceRateResponse>>;
