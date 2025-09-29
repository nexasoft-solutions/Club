namespace NexaSoft.Club.Api.Controllers.Masters.Periodicities.Request;

public sealed record DeletePeriodicityRequest(
   long Id,
   string DeletedBy
);
