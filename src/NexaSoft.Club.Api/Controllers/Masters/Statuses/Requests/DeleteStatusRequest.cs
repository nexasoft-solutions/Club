namespace NexaSoft.Club.Api.Controllers.Masters.Statuses.Request;

public sealed record DeleteStatusRequest(
   long Id,
   string DeletedBy
);
