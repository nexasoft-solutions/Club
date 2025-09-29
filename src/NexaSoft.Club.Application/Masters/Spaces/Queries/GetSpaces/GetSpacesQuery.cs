using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.Spaces;

namespace NexaSoft.Club.Application.Masters.Spaces.Queries.GetSpaces;

public sealed record GetSpacesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SpaceResponse>>;
