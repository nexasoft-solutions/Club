using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.CostCenterTypes;

namespace NexaSoft.Club.Application.HumanResources.CostCenterTypes.Queries.GetCostCenterType;

public sealed record GetCostCenterTypeQuery(
    long Id
) : IQuery<CostCenterTypeResponse>;
