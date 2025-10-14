using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Masters.SpacePhotos;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Queries.GetSpacePhoto;

public sealed record GetSpacePhotoQuery(
    long Id
) : IQuery<SpacePhotoResponse>;
