namespace NexaSoft.Club.Api.Controllers.HumanResources.Currencies.Request;

public sealed record DeleteCurrencyRequest(
   long Id,
   string DeletedBy
);
