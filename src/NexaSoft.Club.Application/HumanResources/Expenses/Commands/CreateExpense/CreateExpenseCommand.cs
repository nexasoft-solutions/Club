using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.CreateExpense;

public sealed record CreateExpenseCommand(
    long? CostCenterId,
    string? Description,
    DateOnly? ExpenseDate,
    decimal Amount,
    string? DocumentNumber,
    string? DocumentPath,
    string CreatedBy
) : ICommand<long>;
