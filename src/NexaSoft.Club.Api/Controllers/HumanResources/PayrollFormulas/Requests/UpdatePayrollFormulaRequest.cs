namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollFormulas.Request;

public sealed record UpdatePayrollFormulaRequest(
   long Id,
    string? Code,
    string? Name,
    string? FormulaExpression,
    string? Description,
    string? RequiredVariables,
    string? Variables,
    string UpdatedBy
);
