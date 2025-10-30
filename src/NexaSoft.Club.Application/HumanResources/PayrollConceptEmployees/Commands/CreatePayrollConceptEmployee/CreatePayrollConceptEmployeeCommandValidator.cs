using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollConceptEmployees.Commands.CreatePayrollConceptEmployee;

public class CreatePayrollConceptEmployeeCommandValidator : AbstractValidator<CreatePayrollConceptEmployeeCommand>
{
    public CreatePayrollConceptEmployeeCommandValidator()
    {
        RuleFor(x => x.PayrollConceptId)
            .GreaterThan(0).WithMessage("Este PayrollConceptId debe ser mayor a cero.");
        // Validación personalizada para PayrollConcept de tipo PayrollConcept
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Este EmployeeId debe ser mayor a cero.");
        // Validación personalizada para EmployeeInfo de tipo EmployeeInfo
    }
}
