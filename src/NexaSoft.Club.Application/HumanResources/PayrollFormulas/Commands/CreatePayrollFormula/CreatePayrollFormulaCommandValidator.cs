using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.PayrollFormulas.Commands.CreatePayrollFormula;

public class CreatePayrollFormulaCommandValidator : AbstractValidator<CreatePayrollFormulaCommand>
{
    public CreatePayrollFormulaCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.FormulaExpression)
            .NotEmpty().WithMessage("El campo FormulaExpression no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        RuleFor(x => x.Variables)
            .NotEmpty().WithMessage("El campo Variables no puede estar vacío.");
    }
}
