using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.Positions;

namespace NexaSoft.Club.Application.HumanResources.Positions.Queries.GetPosition;

public sealed record GetPositionQuery(
    long Id
) : IQuery<PositionResponse>;
