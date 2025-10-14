using FluentValidation;

namespace NexaSoft.Club.Application.Masters.SpaceTypes.Commands.CreateSpaceType;

public class CreateSpaceTypeCommandValidator : AbstractValidator<CreateSpaceTypeCommand>
{
    public CreateSpaceTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
