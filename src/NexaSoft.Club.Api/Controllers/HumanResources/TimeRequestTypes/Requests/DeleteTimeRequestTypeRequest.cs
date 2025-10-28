namespace NexaSoft.Club.Api.Controllers.HumanResources.TimeRequestTypes.Request;

public sealed record DeleteTimeRequestTypeRequest(
   long Id,
   string DeletedBy
);
