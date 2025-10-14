using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Queries.GetSpaceAvailabilities;

public sealed record GetSpaceAvailabilitiesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SpaceAvailabilityResponse>>;
