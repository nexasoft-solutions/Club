using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.HumanResources.Expenses;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Queries.GetExpense;

public sealed record GetExpenseQuery(
    long Id
) : IQuery<ExpenseResponse>;
