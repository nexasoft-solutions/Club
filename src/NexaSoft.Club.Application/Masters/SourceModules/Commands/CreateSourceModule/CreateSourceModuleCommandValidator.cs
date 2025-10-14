using FluentValidation;

namespace NexaSoft.Club.Application.Masters.SourceModules.Commands.CreateSourceModule;

public class CreateSourceModuleCommandValidator : AbstractValidator<CreateSourceModuleCommand>
{
    public CreateSourceModuleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El campo Name no puede estar vacío.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("El campo Description no puede estar vacío.");
    }
}
