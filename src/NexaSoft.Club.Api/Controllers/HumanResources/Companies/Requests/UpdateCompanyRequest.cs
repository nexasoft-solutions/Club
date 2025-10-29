namespace NexaSoft.Club.Api.Controllers.HumanResources.Companies.Request;

public sealed record UpdateCompanyRequest(
   long Id,
    string? Ruc,
    string? BusinessName,
    string? TradeName,
    string? Address,
    string? Phone,
    string? Website,
    string UpdatedBy
);
