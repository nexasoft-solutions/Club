using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Domain.Features.EntryItems;

public class EntryItem : Entity
{
    public long AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public long AccountingChartId { get; private set; }
    public AccountingChart? AccountingChart { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public string? Description { get; private set; }
    public int StateEntryItem { get; private set; }

    private EntryItem() { }

    private EntryItem(
        long entryId, 
        long accountId, 
        decimal debitAmount, 
        decimal creditAmount, 
        string? description, 
        int stateEntryItem, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        AccountingEntryId = entryId;
        AccountingChartId = accountId;
        DebitAmount = debitAmount;
        CreditAmount = creditAmount;
        Description = description;
        StateEntryItem = stateEntryItem;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static EntryItem Create(
        long entryId, 
        long accountId, 
        decimal debitAmount, 
        decimal creditAmount, 
        string? description, 
        int stateEntryItem, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new EntryItem(
            entryId,
            accountId,
            debitAmount,
            creditAmount,
            description,
            stateEntryItem,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long entryId, 
        long accountId, 
        decimal debitAmount, 
        decimal creditAmount, 
        string? description, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        AccountingEntryId = entryId;
        AccountingChartId = accountId;
        DebitAmount = debitAmount;
        CreditAmount = creditAmount;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateEntryItem = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
