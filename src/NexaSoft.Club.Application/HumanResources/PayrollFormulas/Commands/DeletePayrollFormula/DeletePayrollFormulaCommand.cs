using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.DeletePayrollFormula;

public sealed record DeletePayrollFormulaCommand(
    long Id,
    string DeletedBy
) : ICommand<bool>;
