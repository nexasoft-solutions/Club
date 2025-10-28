using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Queries.GetCostCenterTypes;

public sealed record GetCostCenterTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<CostCenterTypeResponse>>;
