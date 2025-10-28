using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Application.HumanResources.CostCenters.Queries.GetCostCenter;

public sealed record GetCostCenterQuery(
    long Id
) : IQuery<CostCenterResponse>;
