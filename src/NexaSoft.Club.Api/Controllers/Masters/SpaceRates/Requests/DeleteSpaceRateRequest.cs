namespace NexaSoft.Club.Api.Controllers.Masters.SpaceRates.Request;

public sealed record DeleteSpaceRateRequest(
   long Id,
   string DeletedBy
);
