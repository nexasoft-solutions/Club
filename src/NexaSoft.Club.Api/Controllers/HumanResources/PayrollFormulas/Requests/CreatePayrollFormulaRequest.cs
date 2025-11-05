namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollFormulas.Request;

public sealed record CreatePayrollFormulaRequest(
    string? Code,
    string? Name,
    string? FormulaExpression,
    string? Description,
    string? RequiredVariables,
    string? Variables,
    string CreatedBy
);
