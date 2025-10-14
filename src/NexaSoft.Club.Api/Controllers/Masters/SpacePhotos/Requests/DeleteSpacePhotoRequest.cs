namespace NexaSoft.Club.Api.Controllers.Masters.SpacePhotos.Request;

public sealed record DeleteSpacePhotoRequest(
   long Id,
   string DeletedBy
);
