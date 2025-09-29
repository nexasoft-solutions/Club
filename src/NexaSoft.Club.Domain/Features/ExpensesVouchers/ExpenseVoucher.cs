using NexaSoft.Club.Domain.Abstractions;
using static NexaSoft.Club.Domain.Shareds.Enums;
using NexaSoft.Club.Domain.Features.AccountingEntries;
using NexaSoft.Club.Domain.Masters.AccountingCharts;

namespace NexaSoft.Club.Domain.Features.ExpensesVouchers;

public class ExpenseVoucher : Entity
{
    public long AccountingEntryId { get; private set; }
    public AccountingEntry? AccountingEntry { get; private set; }
    public string? VoucherNumber { get; private set; }
    public string? SupplierName { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly IssueDate { get; private set; }
    public string? Description { get; private set; }
    public long ExpenseAccountId { get; private set; }
    public AccountingChart? ExpenseAccount { get; private set; }
    public int StateExpenseVoucher { get; private set; }

    private ExpenseVoucher() { }

    private ExpenseVoucher(
        long entryId, 
        string? voucherNumber, 
        string? supplierName, 
        decimal amount, 
        DateOnly issueDate, 
        string? description, 
        long expenseAccountId, 
        int stateExpenseVoucher, 
        DateTime createdAt,
        string? createdBy,
        string? updatedBy = null,
        string? deletedBy = null
    ) : base(createdAt, createdBy, updatedBy, deletedBy)
    {
        AccountingEntryId = entryId;
        VoucherNumber = voucherNumber;
        SupplierName = supplierName;
        Amount = amount;
        IssueDate = issueDate;
        Description = description;
        ExpenseAccountId = expenseAccountId;
        StateExpenseVoucher = stateExpenseVoucher;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
    }

    public static ExpenseVoucher Create(
        long entryId, 
        string? voucherNumber, 
        string? supplierName, 
        decimal amount, 
        DateOnly issueDate, 
        string? description, 
        long expenseAccountId, 
        int stateExpenseVoucher, 
        DateTime createdAd,
        string? createdBy
    )
    {
        var entity = new ExpenseVoucher(
            entryId,
            voucherNumber,
            supplierName,
            amount,
            issueDate,
            description,
            expenseAccountId,
            stateExpenseVoucher,
            createdAd,
            createdBy
        );
        return entity;
    }

    public Result Update(
        long Id,
        long entryId, 
        string? voucherNumber, 
        string? supplierName, 
        decimal amount, 
        DateOnly issueDate, 
        string? description, 
        long expenseAccountId, 
        DateTime utcNow,
        string? updatedBy
    )
    {
        AccountingEntryId = entryId;
        VoucherNumber = voucherNumber;
        SupplierName = supplierName;
        Amount = amount;
        IssueDate = issueDate;
        Description = description;
        ExpenseAccountId = expenseAccountId;
        UpdatedAt = utcNow;
        UpdatedBy = updatedBy;


        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string deletedBy)
    {
        StateExpenseVoucher = (int)EstadosEnum.Eliminado;
        DeletedAt = utcNow;
        DeletedBy = deletedBy;
        return Result.Success();
    }
}
