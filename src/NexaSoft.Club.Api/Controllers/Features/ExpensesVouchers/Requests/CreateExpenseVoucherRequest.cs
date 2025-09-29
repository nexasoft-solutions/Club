namespace NexaSoft.Club.Api.Controllers.Features.ExpensesVouchers.Request;

public sealed record CreateExpenseVoucherRequest(
    long EntryId,
    string? VoucherNumber,
    string? SupplierName,
    decimal Amount,
    DateOnly IssueDate,
    string? Description,
    long ExpenseAccountId,
    string CreatedBy
);
