using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.IncomeTaxScales.Commands.CreateIncomeTaxScale;

public class CreateIncomeTaxScaleCommandValidator : AbstractValidator<CreateIncomeTaxScaleCommand>
{
    public CreateIncomeTaxScaleCommandValidator()
    {
        // Validación personalizada para MaxIncome de tipo decimal
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
