using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.UpdateSpacePhoto;

public sealed record UpdateSpacePhotoCommand(
    long Id,
    long SpaceId,
    string? PhotoUrl,
    int Order,
    string? Description,
    string UpdatedBy
) : ICommand<bool>;
