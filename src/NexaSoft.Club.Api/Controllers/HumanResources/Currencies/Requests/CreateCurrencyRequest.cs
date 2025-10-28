namespace NexaSoft.Club.Api.Controllers.HumanResources.Currencies.Request;

public sealed record CreateCurrencyRequest(
    string? Code,
    string? Name,
    string CreatedBy
);
