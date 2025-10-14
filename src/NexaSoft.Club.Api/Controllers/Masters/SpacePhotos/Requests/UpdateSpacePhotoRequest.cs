namespace NexaSoft.Club.Api.Controllers.Masters.SpacePhotos.Request;

public sealed record UpdateSpacePhotoRequest(
   long Id,
    long SpaceId,
    string? PhotoUrl,
    int Order,
    string? Description,
    string UpdatedBy
);
