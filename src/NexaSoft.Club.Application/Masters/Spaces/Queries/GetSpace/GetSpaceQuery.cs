using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpace;

public sealed record GetSpaceQuery(
    long Id
) : IQuery<SpaceResponse>;
