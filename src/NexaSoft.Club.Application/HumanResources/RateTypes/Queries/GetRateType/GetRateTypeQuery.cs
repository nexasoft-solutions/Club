using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.RateTypes;

namespace NexaSoft.Club.Application.HumanResources.RateTypes.Queries.GetRateType;

public sealed record GetRateTypeQuery(
    long Id
) : IQuery<RateTypeResponse>;
