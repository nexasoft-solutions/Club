using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.Responsables.Commands.CreateResponsable;

public class CreateResponsableCommandValidator : AbstractValidator<CreateResponsableCommand>
{
    public CreateResponsableCommandValidator()
    {
        RuleFor(x => x.NombreResponsable)
            .NotEmpty().WithMessage("El campo NombreResponsable no puede estar vacío.");
        RuleFor(x => x.CargoResponsable)
            .NotEmpty().WithMessage("El campo CargoResponsable no puede estar vacío.");
        RuleFor(x => x.CorreoResponsable)
            .NotEmpty().WithMessage("El campo CorreoResponsable no puede estar vacío.");
        RuleFor(x => x.TelefonoResponsable)
            .NotEmpty().WithMessage("El campo TelefonoResponsable no puede estar vacío.");
        RuleFor(x => x.Observaciones)
            .NotEmpty().WithMessage("El campo Observaciones no puede estar vacío.");
    }
}
