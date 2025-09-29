using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.Features.ExpensesVouchers.Events;

public sealed record ExpenseVoucherUpdateDomainEvent
(
    long Id
): IDomainEvent;
