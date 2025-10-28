namespace NexaSoft.Club.Api.Controllers.HumanResources.Positions.Request;

public sealed record DeletePositionRequest(
   long Id,
   string DeletedBy
);
