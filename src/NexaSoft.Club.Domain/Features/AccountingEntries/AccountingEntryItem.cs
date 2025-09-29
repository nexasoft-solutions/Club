using NexaSoft.Club.Domain.Abstractions;
using NexaSoft.Club.Domain.Masters.AccountingCharts;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Features.AccountingEntries;

public class AccountingEntryItem: Entity
{
    public long AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public long AccountingChartId { get; private set; }
    public AccountingChart? AccountingChart { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public string? Description { get; private set; }
    public int StateEntryItem { get; private set; }

    private AccountingEntryItem() { }

    private AccountingEntryItem(
        long accountingEntryId,
        long accountingChartId,
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
        AccountingEntryId = accountingEntryId;
        AccountingChartId = accountingChartId;
        DebitAmount = debitAmount;
        CreditAmount = creditAmount;
        Description = description;
        StateEntryItem = stateEntryItem;
    }

    public static AccountingEntryItem Create(
        long accountingEntryId,
        long accountingChartId,
        decimal debitAmount,
        decimal creditAmount,
        string? description,
        int stateEntryItem,
        DateTime createdAt,
        string? createdBy
    )
    {
        // Validar que solo haya débito o crédito, no ambos
        if (debitAmount > 0 && creditAmount > 0)
            throw new ArgumentException("Un item no puede tener débito y crédito simultáneamente");

        if (debitAmount < 0 || creditAmount < 0)
            throw new ArgumentException("Los montos de débito y crédito no pueden ser negativos");

        var entity = new AccountingEntryItem(
            accountingEntryId,
            accountingChartId,
            debitAmount,
            creditAmount,
            description,
            stateEntryItem,
            createdAt,
            createdBy
        );

        return entity;
    }

    public Result Update(
        long accountingChartId,
        decimal debitAmount,
        decimal creditAmount,
        string? description,
        DateTime utcNow,
        string? updatedBy
    )
    {
        // Validaciones
        /*if (debitAmount > 0 && creditAmount > 0)
            return Result.Failure("Un item no puede tener débito y crédito simultáneamente");

        if (debitAmount < 0 || creditAmount < 0)
            return Result.Failure("Los montos de débito y crédito no pueden ser negativos");*/

        AccountingChartId = accountingChartId;
        DebitAmount = debitAmount;
        CreditAmount = creditAmount;
        Description = description;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateEntryItem = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}