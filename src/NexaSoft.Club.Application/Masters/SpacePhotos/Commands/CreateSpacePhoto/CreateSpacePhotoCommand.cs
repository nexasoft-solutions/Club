using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Masters.SpacePhotos.Commands.CreateSpacePhoto;

public sealed record CreateSpacePhotoCommand(
    long SpaceId,
    Stream PhotoFile,
    string OriginalFileName,    // Nuevo parámetro
    string ContentType,         // Nuevo parámetro
    int Order,
    string? Description,
    string CreatedBy
) : ICommand<long>;
