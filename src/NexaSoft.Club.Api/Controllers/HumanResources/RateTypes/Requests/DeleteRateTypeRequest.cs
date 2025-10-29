namespace NexaSoft.Club.Api.Controllers.HumanResources.RateTypes.Request;

public sealed record DeleteRateTypeRequest(
   long Id,
   string DeletedBy
);
