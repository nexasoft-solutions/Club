using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.UpdateExpenseVoucher;

public sealed record UpdateExpenseVoucherCommand(
    long Id,
    long EntryId,
    string? VoucherNumber,
    string? SupplierName,
    decimal Amount,
    DateOnly IssueDate,
    string? Description,
    long ExpenseAccountId,
    string UpdatedBy
) : ICommand<bool>;
