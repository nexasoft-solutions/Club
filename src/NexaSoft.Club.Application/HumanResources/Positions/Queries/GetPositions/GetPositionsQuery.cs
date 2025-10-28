using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.Positions;

namespace NexaSoft.Club.Application.HumanResources.Positions.Queries.GetPositions;

public sealed record GetPositionsQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PositionResponse>>;
