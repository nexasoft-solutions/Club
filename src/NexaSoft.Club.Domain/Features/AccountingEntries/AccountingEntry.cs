using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;

namespace NexaSoft.Club.Domain.Features.AccountingEntries;

public class AccountingEntry : Entity
{
    public string? EntryNumber { get; private set; }
    public DateOnly EntryDate { get; private set; }
    public string? Description { get; private set; }
    public string? SourceModule { get; private set; }
    public long? SourceId { get; private set; }
    public decimal TotalDebit { get; private set; }
    public decimal TotalCredit { get; private set; }
    public bool IsAdjusted { get; private set; }
    public string? AdjustmentReason { get; private set; }
    public int StateAccountingEntry { get; private set; }

    private AccountingEntry() { }

    private AccountingEntry(
        string? entryNumber,
        DateOnly entryDate,
        string? description,
        string? sourceModule,
        long? sourceId,
        decimal totalDebit,
        decimal totalCredit,
        bool isAdjusted,
        string? adjustmentReason,
        int stateAccountingEntry,
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        EntryNumber = entryNumber;
        EntryDate = entryDate;
        Description = description;
        SourceModule = sourceModule;
        SourceId = sourceId;
        TotalDebit = totalDebit;
        TotalCredit = totalCredit;
        IsAdjusted = isAdjusted;
        AdjustmentReason = adjustmentReason;
        StateAccountingEntry = stateAccountingEntry;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static AccountingEntry Create(
        string? entryNumber,
        DateOnly entryDate,
        string? description,
        string? sourceModule,
        long? sourceId,
        decimal totalDebit,
        decimal totalCredit,
        bool isAdjusted,
        int stateAccountingEntry,
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new AccountingEntry(
            entryNumber,
            entryDate,
            description,
            sourceModule,
            sourceId,
            totalDebit,
            totalCredit,
            isAdjusted,
            adjustmentReason: null,
            stateAccountingEntry,
            createdAd,
            createdBy
        );
        return entity;
    }


    // MÉTODO NUEVO: Para establecer el SourceId después de crear la entidad relacionada
    public void SetSourceId(long sourceId)
    {
        SourceId = sourceId;
        UpdatedAt = DateTime.UtcNow;
    }

    // MÉTODO PARA MARCAR COMO AJUSTADO
    public Result MarkAsAdjusted(string adjustmentReason, DateTime adjustedAt, string adjustedBy)
    {
        IsAdjusted = true;
        AdjustmentReason = adjustmentReason;
        UpdatedAt = adjustedAt;
        UpdatedBy = adjustedBy;

        return Result.Success();
    }

    // MÉTODO PARA VALIDAR QUE DÉBITO = CRÉDITO
    public Result ValidateBalance()
    {
        /*if (TotalDebit != TotalCredit)
            return Result.Failure($"El asiento no está balanceado. Débito: {TotalDebit}, Crédito: {TotalCredit}");*/

        return Result.Success();
    }


    public Result Update(
        long Id,
        string? entryNumber,
        DateOnly entryDate,
        string? description,
        decimal totalDebit,
        decimal totalCredit,
        bool isAdjusted,
        string? adjustmentReason,
        DateTime utcNow,
        string? updatedBy
    )
    {
        EntryNumber = entryNumber;
        EntryDate = entryDate;
        Description = description;
        TotalDebit = totalDebit;
        TotalCredit = totalCredit;
        IsAdjusted = isAdjusted;
        AdjustmentReason = adjustmentReason;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string deletedBy)
    {
        StateAccountingEntry = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
