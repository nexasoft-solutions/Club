namespace NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

public sealed record CompanyBankAccountResponse(
    long Id,
    long? CompanyId,
    string? BusinessName,
    long? BankId,
    string? BankCode,
    long? BankAccountTypeId,
    string? BankAccountTypeCode,
    string? BankAccountNumber,
    string? CciNumber,
    long? CurrencyId,
    string? CurrencyCode,
    bool IsPrimary,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
