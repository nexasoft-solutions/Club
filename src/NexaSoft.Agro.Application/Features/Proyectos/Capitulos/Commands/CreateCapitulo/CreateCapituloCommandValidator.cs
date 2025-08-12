using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.Capitulos.Commands.CreateCapitulo;

public class CreateCapituloCommandValidator : AbstractValidator<CreateCapituloCommand>
{
    public CreateCapituloCommandValidator()
    {
        RuleFor(x => x.NombreCapitulo)
            .NotEmpty().WithMessage("El campo NombreCapitulo no puede estar vacío.");
        RuleFor(x => x.DescripcionCapitulo)
            .NotEmpty().WithMessage("El campo DescripcionCapitulo no puede estar vacío.");
        // Validación personalizada para EstudioAmbiental de tipo EstudioAmbiental
    }
}
