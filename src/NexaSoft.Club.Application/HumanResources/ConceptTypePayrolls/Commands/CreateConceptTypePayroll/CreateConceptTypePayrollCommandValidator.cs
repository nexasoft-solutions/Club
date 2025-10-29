using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.ConceptTypePayrolls.Commands.CreateConceptTypePayroll;

public class CreateConceptTypePayrollCommandValidator : AbstractValidator<CreateConceptTypePayrollCommand>
{
    public CreateConceptTypePayrollCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
