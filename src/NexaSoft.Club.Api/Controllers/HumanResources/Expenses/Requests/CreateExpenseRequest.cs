namespace NexaSoft.Club.Api.Controllers.HumanResources.Expenses.Request;

public sealed record CreateExpenseRequest(
    long? CostCenterId,
    string? Description,
    DateOnly? ExpenseDate,
    decimal Amount,
    string? DocumentNumber,
    string? DocumentPath,
    string CreatedBy
);
