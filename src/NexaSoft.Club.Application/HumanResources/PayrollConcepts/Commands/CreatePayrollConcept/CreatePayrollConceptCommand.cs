using NexaSoft.Club.Application.Abstractions.Messaging;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConcept;

public sealed record CreatePayrollConceptCommand(
    string? Code,
    string? Name,
    long? ConceptTypePayrollId,
    long? PayrollFormulaId,
    long? ConceptCalculationTypeId,
    decimal? FixedValue,
    decimal? PorcentajeValue,
    long? ConceptApplicationTypesId,
    long? AccountingChartId,
    //long? PayrollTypeId,
    string CreatedBy
) : ICommand<long>;
