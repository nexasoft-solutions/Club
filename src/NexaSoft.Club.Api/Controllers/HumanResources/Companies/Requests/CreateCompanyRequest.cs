namespace NexaSoft.Club.Api.Controllers.HumanResources.Companies.Request;

public sealed record CreateCompanyRequest(
    string? Ruc,
    string? BusinessName,
    string? TradeName,
    string? Address,
    string? Phone,
    string? Website,
    string CreatedBy
);
