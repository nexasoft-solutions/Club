using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptDepartments.Commands.CreatePayrollConceptDepartment;

public class CreatePayrollConceptDepartmentCommandValidator : AbstractValidator<CreatePayrollConceptDepartmentCommand>
{
    public CreatePayrollConceptDepartmentCommandValidator()
    {
        RuleFor(x => x.PayrollConceptId)
            .GreaterThan(0).WithMessage("Este PayrollConceptId debe ser mayor a cero.");
        // Validación personalizada para PayrollConcept de tipo PayrollConcept
        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Este DepartmentId debe ser mayor a cero.");
        // Validación personalizada para Department de tipo Department
    }
}
