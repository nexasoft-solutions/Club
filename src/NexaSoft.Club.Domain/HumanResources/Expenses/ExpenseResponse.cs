namespace NexaSoft.Club.Domain.HumanResources.Expenses;

public sealed record ExpenseResponse(
    long Id,
    long? CostCenterId,
    string? CostCenterCode,
    string? Description,
    DateOnly? ExpenseDate,
    decimal Amount,
    string? DocumentNumber,
    string? DocumentPath,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
