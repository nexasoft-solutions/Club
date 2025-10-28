using FluentValidation;

namespace NexaSoft.Club.Application.HumanResources.TimeRequestTypes.Commands.CreateTimeRequestType;

public class CreateTimeRequestTypeCommandValidator : AbstractValidator<CreateTimeRequestTypeCommand>
{
    public CreateTimeRequestTypeCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El campo Code no puede estar vacío.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
