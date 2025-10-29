using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.SpecialRates;

namespace NexaSoft.Club.Application.HumanResources.SpecialRates.Queries.GetSpecialRate;

public sealed record GetSpecialRateQuery(
    long Id
) : IQuery<SpecialRateResponse>;
