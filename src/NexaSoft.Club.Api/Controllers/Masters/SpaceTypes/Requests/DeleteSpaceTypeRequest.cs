namespace NexaSoft.Club.Api.Controllers.Masters.SpaceTypes.Request;

public sealed record DeleteSpaceTypeRequest(
   long Id,
   string DeletedBy
);
