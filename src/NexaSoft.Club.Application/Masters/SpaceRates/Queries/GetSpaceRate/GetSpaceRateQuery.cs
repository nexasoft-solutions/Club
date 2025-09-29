using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SpaceRates;

namespace NexaSoft.Club.Application.Masters.SpaceRates.Queries.GetSpaceRate;

public sealed record GetSpaceRateQuery(
    long Id
) : IQuery<SpaceRateResponse>;
