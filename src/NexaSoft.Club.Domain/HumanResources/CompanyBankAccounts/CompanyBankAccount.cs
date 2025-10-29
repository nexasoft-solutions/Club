using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.Companies;
using NexaSoft.Club.Domain.HumanResources.Banks;
using NexaSoft.Club.Domain.HumanResources.BankAccountTypes;
using NexaSoft.Club.Domain.HumanResources.Currencies;

namespace NexaSoft.Club.Domain.HumanResources.CompanyBankAccounts;

public class CompanyBankAccount : Entity
{
    public long? CompanyId { get; private set; }
    public Company? Company { get; private set; }
    public long? BankId { get; private set; }
    public Bank? Bank { get; private set; }
    public long? BankAccountTypeId { get; private set; }
    public BankAccountType? BankAccountType { get; private set; }
    public string? BankAccountNumber { get; private set; }
    public string? CciNumber { get; private set; }
    public long? CurrencyId { get; private set; }
    public Currency? Currency { get; private set; }
    public bool IsPrimary { get; private set; }
    public bool IsActive { get; private set; }
    public int StateCompanyBankAccount { get; private set; }

    private CompanyBankAccount() { }

    private CompanyBankAccount(
        long? companyId, 
        long? bankId, 
        long? bankAccountTypeId, 
        string? bankAccountNumber, 
        string? cciNumber, 
        long? currencyId, 
        bool isPrimary, 
        bool isActive, 
        int stateCompanyBankAccount, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        CompanyId = companyId;
        BankId = bankId;
        BankAccountTypeId = bankAccountTypeId;
        BankAccountNumber = bankAccountNumber;
        CciNumber = cciNumber;
        CurrencyId = currencyId;
        IsPrimary = isPrimary;
        IsActive = isActive;
        StateCompanyBankAccount = stateCompanyBankAccount;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static CompanyBankAccount Create(
        long? companyId, 
        long? bankId, 
        long? bankAccountTypeId, 
        string? bankAccountNumber, 
        string? cciNumber, 
        long? currencyId, 
        bool isPrimary, 
        bool isActive, 
        int stateCompanyBankAccount, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new CompanyBankAccount(
            companyId,
            bankId,
            bankAccountTypeId,
            bankAccountNumber,
            cciNumber,
            currencyId,
            isPrimary,
            isActive,
            stateCompanyBankAccount,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? companyId, 
        long? bankId, 
        long? bankAccountTypeId, 
        string? bankAccountNumber, 
        string? cciNumber, 
        long? currencyId, 
        bool isPrimary, 
        bool isActive, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        CompanyId = companyId;
        BankId = bankId;
        BankAccountTypeId = bankAccountTypeId;
        BankAccountNumber = bankAccountNumber;
        CciNumber = cciNumber;
        CurrencyId = currencyId;
        IsPrimary = isPrimary;
        IsActive = isActive;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateCompanyBankAccount = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
