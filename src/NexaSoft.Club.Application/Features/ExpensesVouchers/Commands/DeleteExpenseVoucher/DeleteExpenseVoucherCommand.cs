using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Commands.DeleteExpenseVoucher;

public sealed record DeleteExpenseVoucherCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
