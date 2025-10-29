namespace NexaSoft.Club.Domain.HumanResources.PayrollFormulas;

public sealed record PayrollFormulaResponse(
    long Id,
    string? Code,
    string? Name,
    string? FormulaExpression,
    string? Description,
    string? Variables,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
