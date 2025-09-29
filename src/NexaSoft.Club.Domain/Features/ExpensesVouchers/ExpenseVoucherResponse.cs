namespace NexaSoft.Club.Domain.Features.ExpensesVouchers;

public sealed record ExpenseVoucherResponse(
    long Id,
    long? AccountingEntryId,
    string? EntryNumber,
    string? VoucherNumber,
    string? SupplierName,
    decimal Amount,
    DateOnly IssueDate,
    string? Description,
    long ExpenseAccountId,
    string? AccountName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
