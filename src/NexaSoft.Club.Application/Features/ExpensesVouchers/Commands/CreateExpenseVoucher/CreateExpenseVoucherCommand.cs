using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.CreateExpenseVoucher;

public sealed record CreateExpenseVoucherCommand(
    long EntryId,
    string? VoucherNumber,
    string? SupplierName,
    decimal Amount,
    DateOnly IssueDate,
    string? Description,
    long ExpenseAccountId,
    string CreatedBy
) : ICommand<long>;

