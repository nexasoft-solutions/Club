using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SpaceTypes;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Queries.GetSpaceType;

public sealed record GetSpaceTypeQuery(
    long Id
) : IQuery<SpaceTypeResponse>;
