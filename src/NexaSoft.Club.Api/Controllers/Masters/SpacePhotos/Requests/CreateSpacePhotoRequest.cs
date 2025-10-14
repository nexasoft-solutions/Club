namespace NexaSoft.Club.Api.Controllers.Masters.SpacePhotos.Request;

public sealed record CreateSpacePhotoRequest(
    long SpaceId,
    IFormFile PhotoFile,
    int Order,
    string? Description,
    string CreatedBy
);
