using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.SpaceTypes;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Queries.GetSpaceTypes;

public sealed record GetSpaceTypesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SpaceTypeResponse>>;
