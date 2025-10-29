using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.Expenses.Commands.UpdateExpense;

public sealed record UpdateExpenseCommand(
    long Id,
    long? CostCenterId,
    string? Description,
    DateOnly? ExpenseDate,
    decimal Amount,
    string? DocumentNumber,
    string? DocumentPath,
    string UpdatedBy
) : ICommand<bool>;
