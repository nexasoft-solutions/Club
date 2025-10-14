using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SpaceAvailabilities;

namespace NexaSoft.Club.Application.Masters.SpaceAvailabilities.Queries.GetSpaceAvailability;

public sealed record GetSpaceAvailabilityQuery(
    long Id
) : IQuery<SpaceAvailabilityResponse>;
