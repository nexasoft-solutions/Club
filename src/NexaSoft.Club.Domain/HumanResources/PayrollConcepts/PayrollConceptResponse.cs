namespace NexaSoft.Club.Domain.HumanResources.PayrollConcepts;

public sealed record PayrollConceptResponse(
    long Id,
    string? Code,
    string? Name,
    long? ConceptTypePayrollId,
    string? ConceptTypePayrollCode,
    long? PayrollFormulaId,
    string? PayrollFormulaCode,
    long? ConceptCalculationTypeId,
    string? ConceptCalculationTypeCode,
    decimal FixedValue,
    decimal PorcentajeValue,
    long? ConceptApplicationTypesId,
    string? ConceptApplicationTypeCode,
    long? AccountingChartId,
    string? AccountName,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy
);
