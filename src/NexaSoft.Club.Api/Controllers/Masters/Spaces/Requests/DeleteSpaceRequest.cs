namespace NexaSoft.Club.Api.Controllers.Masters.Spaces.Request;

public sealed record DeleteSpaceRequest(
   long Id,
   string DeletedBy
);
