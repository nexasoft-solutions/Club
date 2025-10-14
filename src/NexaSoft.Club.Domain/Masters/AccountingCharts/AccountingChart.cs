using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountTypes;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Masters.AccountingCharts;

public class AccountingChart : Entity
{
    public string? AccountCode { get; private set; }
    public string? AccountName { get; private set; }
    public long AccountTypeId { get; private set; }
    public AccountType? AccountType { get; set; }
    public long? ParentAccountId { get; private set; }
    public AccountingChart? ParentAccount { get; private set; }
    public int Level { get; private set; }
    public bool AllowsTransactions { get; private set; }
    public string? Description { get; private set; }
    public int StateAccountingChart { get; private set; }

    private AccountingChart() { }

    private AccountingChart(
        string? accountCode, 
        string? accountName, 
        long accountTypeId, 
        long? parentAccountId, 
        int level, 
        bool allowsTransactions, 
        string? description, 
        int stateAccountingChart, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        AccountCode = accountCode;
        AccountName = accountName;
        AccountTypeId = accountTypeId;
        ParentAccountId = parentAccountId;
        Level = level;
        AllowsTransactions = allowsTransactions;
        Description = description;
        StateAccountingChart = stateAccountingChart;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static AccountingChart Create(
        string? accountCode, 
        string? accountName, 
        long accountTypeId, 
        long? parentAccountId, 
        int level, 
        bool allowsTransactions, 
        string? description, 
        int stateAccountingChart, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new AccountingChart(
            accountCode,
            accountName,
            accountTypeId,
            parentAccountId,
            level,
            allowsTransactions,
            description,
            stateAccountingChart,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        string? accountCode, 
        string? accountName, 
        long accountTypeId, 
        long? parentAccountId, 
        int level, 
        bool allowsTransactions, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        AccountCode = accountCode;
        AccountName = accountName;
        AccountTypeId = accountTypeId;
        ParentAccountId = parentAccountId;
        Level = level;
        AllowsTransactions = allowsTransactions;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateAccountingChart = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
