namespace NexaSoft.Club.Api.Controllers.HumanResources.CompanyBankAccounts.Request;

public sealed record CreateCompanyBankAccountRequest(
    long? CompanyId,
    long? BankId,
    long? BankAccountTypeId,
    string? BankAccountNumber,
    string? CciNumber,
    long? CurrencyId,
    bool IsPrimary,
    bool IsActive,
    string CreatedBy
);
