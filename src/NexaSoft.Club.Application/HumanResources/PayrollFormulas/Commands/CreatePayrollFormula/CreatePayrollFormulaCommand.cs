using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.CreatePayrollFormula;

public sealed record CreatePayrollFormulaCommand(
    string? Code,
    string? Name,
    string? FormulaExpression,
    string? Description,
    string? Variables,
    string CreatedBy
) : ICommand<long>;
