namespace NexaSoft.Club.Domain.Masters.SpacePhotos;

public sealed record SpacePhotoResponse(
    long Id,
    long SpaceId,
    string? SpaceName,
    string? PhotoUrl,
    int Order,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
