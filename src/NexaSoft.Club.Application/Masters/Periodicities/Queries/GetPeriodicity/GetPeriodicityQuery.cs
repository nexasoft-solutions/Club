using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Periodicities;

namespace NexaSoft.Club.Application.Masters.Periodicities.Queries.GetPeriodicity;

public sealed record GetPeriodicityQuery(
    long Id
) : IQuery<PeriodicityResponse>;
