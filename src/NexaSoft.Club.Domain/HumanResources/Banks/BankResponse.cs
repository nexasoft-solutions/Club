namespace NexaSoft.Club.Domain.HumanResources.Banks;

public sealed record BankResponse(
    long Id,
    string? Code,
    string? Name,
    string? WebSite,
    string? Phone,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
