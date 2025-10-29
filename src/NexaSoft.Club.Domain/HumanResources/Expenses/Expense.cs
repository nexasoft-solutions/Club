using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.HumanResources.CostCenters;

namespace NexaSoft.Club.Domain.HumanResources.Expenses;

public class Expense : Entity
{
    public long? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }
    public string? Description { get; private set; }
    public DateOnly? ExpenseDate { get; private set; }
    public decimal Amount { get; private set; }
    public string? DocumentNumber { get; private set; }
    public string? DocumentPath { get; private set; }
    public int StateExpense { get; private set; }

    private Expense() { }

    private Expense(
        long? costCenterId, 
        string? description, 
        DateOnly? expenseDate, 
        decimal amount, 
        string? documentNumber, 
        string? documentPath, 
        int stateExpense, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        CostCenterId = costCenterId;
        Description = description;
        ExpenseDate = expenseDate;
        Amount = amount;
        DocumentNumber = documentNumber;
        DocumentPath = documentPath;
        StateExpense = stateExpense;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static Expense Create(
        long? costCenterId, 
        string? description, 
        DateOnly? expenseDate, 
        decimal amount, 
        string? documentNumber, 
        string? documentPath, 
        int stateExpense, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new Expense(
            costCenterId,
            description,
            expenseDate,
            amount,
            documentNumber,
            documentPath,
            stateExpense,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long? costCenterId, 
        string? description, 
        DateOnly? expenseDate, 
        decimal amount, 
        string? documentNumber, 
        string? documentPath, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        CostCenterId = costCenterId;
        Description = description;
        ExpenseDate = expenseDate;
        Amount = amount;
        DocumentNumber = documentNumber;
        DocumentPath = documentPath;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateExpense = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
