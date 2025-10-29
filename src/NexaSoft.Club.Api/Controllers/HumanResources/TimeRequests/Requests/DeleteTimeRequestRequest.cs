namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequests.Request;

public sealed record DeleteTimeRequestRequest(
   long Id,
   string DeletedBy
);
