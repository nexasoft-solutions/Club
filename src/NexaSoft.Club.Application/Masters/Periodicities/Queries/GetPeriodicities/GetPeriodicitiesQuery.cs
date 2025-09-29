using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Periodicities;

namespace NexaSoft.Club.Application.Masters.Periodicities.Queries.GetPeriodicities;

public sealed record GetPeriodicitiesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<PeriodicityResponse>>;
