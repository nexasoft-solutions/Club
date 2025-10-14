using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Masters.SpacePhotos.Events;

public sealed record SpacePhotoCreateDomainEvent
(
    long Id
): IDomainEvent;
