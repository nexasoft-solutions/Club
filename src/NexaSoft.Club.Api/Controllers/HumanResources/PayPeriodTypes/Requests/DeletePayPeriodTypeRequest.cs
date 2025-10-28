namespace NexaSoft.Club.Api.Controllers.HumanResources.PayPeriodTypes.Request;

public sealed record DeletePayPeriodTypeRequest(
   long Id,
   string DeletedBy
);
