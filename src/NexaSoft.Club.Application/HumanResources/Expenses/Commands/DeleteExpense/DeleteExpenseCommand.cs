using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.DeleteExpense;

public sealed record DeleteExpenseCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
