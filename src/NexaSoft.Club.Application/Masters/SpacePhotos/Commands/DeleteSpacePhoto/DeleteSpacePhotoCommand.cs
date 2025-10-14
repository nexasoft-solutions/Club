using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.DeleteSpacePhoto;

public sealed record DeleteSpacePhotoCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
