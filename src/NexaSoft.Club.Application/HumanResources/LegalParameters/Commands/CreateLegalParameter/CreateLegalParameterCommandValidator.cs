using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.LegalParameters.Commands.CreateLegalParameter;

public class CreateLegalParameterCommandValidator : AbstractValidator<CreateLegalParameterCommand>
{
    public CreateLegalParameterCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.ValueText)
            .NotEmpty().WithMessage("El campo ValueText no puede estar vacío.");
        // Validación personalizada para EndDate de tipo DateOnly
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("El campo Category no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
