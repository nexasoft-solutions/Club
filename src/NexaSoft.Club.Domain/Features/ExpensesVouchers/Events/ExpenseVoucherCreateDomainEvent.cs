using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.ExpensesVouchers.Events;

public sealed record ExpenseVoucherCreateDomainEvent
(
    long Id
): IDomainEvent;
