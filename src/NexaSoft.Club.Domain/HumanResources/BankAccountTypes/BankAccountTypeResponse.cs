namespace NexaSoft.Club.Domain.HumanResources.BankAccountTypes;

public sealed record BankAccountTypeResponse(
    long Id,
    string? Code,
    string? Name,
    string? Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
