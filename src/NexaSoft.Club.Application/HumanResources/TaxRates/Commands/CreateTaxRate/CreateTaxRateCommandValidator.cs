using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.TaxRates.Commands.CreateTaxRate;

public class CreateTaxRateCommandValidator : AbstractValidator<CreateTaxRateCommand>
{
    public CreateTaxRateCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.RateType)
            .NotEmpty().WithMessage("El campo RateType no puede estar vacío.");
        // Validación personalizada para MinAmount de tipo decimal
        // Validación personalizada para MaxAmount de tipo decimal
        // Validación personalizada para EndDate de tipo DateOnly
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("El campo Category no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
        RuleFor(x => x.AppliesTo)
            .NotEmpty().WithMessage("El campo AppliesTo no puede estar vacío.");
    }
}
