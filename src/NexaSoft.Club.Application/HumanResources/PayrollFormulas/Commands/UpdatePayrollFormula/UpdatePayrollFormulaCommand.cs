using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.UpdatePayrollFormula;

public sealed record UpdatePayrollFormulaCommand(
    long Id,
    string? Code,
    string? Name,
    string? FormulaExpression,
    string? Description,
    string? RequiredVariables,
    string? Variables,
    string UpdatedBy
) : ICommand<bool>;
