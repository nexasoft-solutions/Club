using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Queries.GetCostCenters;

public sealed record GetCostCentersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CostCenterResponse>>;
