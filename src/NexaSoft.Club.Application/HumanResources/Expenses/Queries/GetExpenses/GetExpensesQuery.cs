using NexaSoft.Club.Application.Abstractions.Messaging;
using NexaSoft.Club.Domain.Specifications;
using NexaSoft.Club.Application.Abstractions.RequestHelpers;
using NexaSoft.Club.Domain.HumanResources.Expenses;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Queries.GetExpenses;

public sealed record GetExpensesQuery(
    BaseSpecParams SpecParams
) : IQuery<Pagination<ExpenseResponse>>;
