using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Masters.SpacePhotos;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Queries.GetSpacePhotos;

public sealed record GetSpacePhotosQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<SpacePhotoResponse>>;
