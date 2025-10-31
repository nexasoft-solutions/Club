namespace NexaSoft.Club.Api.Controllers.HumanResources.PayrollConcepts.Request;

public sealed record UpdatePayrollConceptRequest(
   long Id,
    string? Code,
    string? Name,
    long? ConceptTypePayrollId,
    long? PayrollFormulaId,
    long? ConceptCalculationTypeId,
    decimal? FixedValue,
    decimal? PorcentajeValue,
    long? ConceptApplicationTypesId,
    long? AccountingChartId,
    long? PayrollTypeId,
    string UpdatedBy
);
