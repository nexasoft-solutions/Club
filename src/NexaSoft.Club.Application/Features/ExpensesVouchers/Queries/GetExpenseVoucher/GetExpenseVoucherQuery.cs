using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Queries.GetExpenseVoucher;

public sealed record GetExpenseVoucherQuery(
    long Id
) : IQuery<ExpenseVoucherResponse>;
