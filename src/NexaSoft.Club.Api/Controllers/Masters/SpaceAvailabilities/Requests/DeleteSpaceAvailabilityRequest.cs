namespace NexaSoft.Club.Api.Controllers.Masters.SpaceAvailabilities.Request;

public sealed record DeleteSpaceAvailabilityRequest(
   long Id,
   string DeletedBy
);
