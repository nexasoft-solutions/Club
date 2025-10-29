namespace NexaSoft.Club.Domain.HumanResources.Companies;

public sealed record CompanyResponse(
    long Id,
    string? Ruc,
    string? BusinessName,
    string? TradeName,
    string? Address,
    string? Phone,
    string? Website,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
