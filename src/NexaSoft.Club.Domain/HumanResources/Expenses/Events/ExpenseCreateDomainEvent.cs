using NexaSoft.Club.Domain.Abstractions;

namespace NexaSoft.Club.Domain.HumanResources.Expenses.Events;

public sealed record ExpenseCreateDomainEvent
(
    long Id
): IDomainEvent;
