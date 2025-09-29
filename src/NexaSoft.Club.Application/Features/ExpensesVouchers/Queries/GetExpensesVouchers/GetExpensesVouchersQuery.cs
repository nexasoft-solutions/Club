using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.Features.ExpensesVouchers;

namespace NexaSoft.Club.Application.Features.ExpensesVouchers.Queries.GetExpensesVouchers;

public sealed record GetExpensesVouchersQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ExpenseVoucherResponse>>;
