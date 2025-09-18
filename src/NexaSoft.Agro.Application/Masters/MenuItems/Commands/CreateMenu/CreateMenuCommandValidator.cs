using FluentValidation;

namespace NexaSoft.Agro.Application.Masters.MenuItems.Commands.CreateMenu;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(x => x.Label)
            .NotEmpty().WithMessage("El campo Label no puede estar vacío.");
        /*RuleFor(x => x.Route)
            .NotEmpty().WithMessage("El campo ruta no puede estar vacío.");*/
        RuleFor(x => x.Icon)
            .NotEmpty().WithMessage("El campo icono no puede estar vacío.");
    }
}
