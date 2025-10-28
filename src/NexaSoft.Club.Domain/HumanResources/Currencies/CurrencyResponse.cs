namespace NexaSoft.Club.Domain.HumanResources.Currencies;

public sealed record CurrencyResponse(
    long Id,
    string? Code,
    string? Name,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
