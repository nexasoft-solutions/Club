using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.UpdatePayrollConcept;

public sealed record UpdatePayrollConceptCommand(
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
) : ICommand<bool>;
