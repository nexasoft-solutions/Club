using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.RateTypes;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Queries.GetRateTypes;

public sealed record GetRateTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<RateTypeResponse>>;
