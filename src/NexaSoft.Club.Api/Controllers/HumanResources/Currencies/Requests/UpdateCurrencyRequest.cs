namespace NexaSoft.Club.Api.Controllers.HumanResources.Currencies.Request;

public sealed record UpdateCurrencyRequest(
   long Id,
    string? Code,
    string? Name,
    string UpdatedBy
);
