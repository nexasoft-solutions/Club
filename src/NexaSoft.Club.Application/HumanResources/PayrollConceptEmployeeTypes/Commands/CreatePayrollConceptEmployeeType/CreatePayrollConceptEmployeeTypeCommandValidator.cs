using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployeeTypes.Commands.CreatePayrollConceptEmployeeType;

public class CreatePayrollConceptEmployeeTypeCommandValidator : AbstractValidator<CreatePayrollConceptEmployeeTypeCommand>
{
    public CreatePayrollConceptEmployeeTypeCommandValidator()
    {
        RuleFor(x => x.PayrollConceptId)
            .GreaterThan(0).WithMessage("Este PayrollConceptId debe ser mayor a cero.");
        // Validación personalizada para PayrollConcept de tipo PayrollConcept
        RuleFor(x => x.EmployeeTypeId)
            .GreaterThan(0).WithMessage("Este EmployeeTypeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeType de tipo EmployeeType
    }
}
