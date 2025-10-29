using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollConcepts.Commands.CreatePayrollConcept;

public class CreatePayrollConceptCommandValidator : AbstractValidator<CreatePayrollConceptCommand>
{
    public CreatePayrollConceptCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.ConceptTypePayrollId)
            .GreaterThan(0).WithMessage("Este ConceptTypePayrollId debe ser mayor a cero.");
        // Validación personalizada para ConceptTypePayroll de tipo ConceptTypePayroll
        RuleFor(x => x.PayrollFormulaId)
            .GreaterThan(0).WithMessage("Este PayrollFormulaId debe ser mayor a cero.");
        // Validación personalizada para PayrollFormula de tipo PayrollFormula
        RuleFor(x => x.ConceptCalculationTypeId)
            .GreaterThan(0).WithMessage("Este ConceptCalculationTypeId debe ser mayor a cero.");
        // Validación personalizada para ConceptCalculationType de tipo ConceptCalculationType
        RuleFor(x => x.ConceptApplicationTypesId)
            .GreaterThan(0).WithMessage("Este ConceptApplicationTypesId debe ser mayor a cero.");
        // Validación personalizada para ConceptApplicationType de tipo ConceptApplicationType
        RuleFor(x => x.AccountingChartId)
            .GreaterThan(0).WithMessage("Este AccountingChartId debe ser mayor a cero.");
        // Validación personalizada para AccountingChart de tipo AccountingChart
    }
}
