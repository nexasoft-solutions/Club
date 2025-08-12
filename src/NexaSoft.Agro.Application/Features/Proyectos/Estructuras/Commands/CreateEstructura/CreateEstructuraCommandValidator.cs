using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.Estructuras.Commands.CreateEstructura;

public class CreateEstructuraCommandValidator : AbstractValidator<CreateEstructuraCommand>
{
    public CreateEstructuraCommandValidator()
    {
        RuleFor(x => x.DescripcionEstructura)
            .NotEmpty().WithMessage("El campo DescripcionEstructura no puede estar vacío.");
        // Validación personalizada para SubCapitulo de tipo SubCapitulo
    }
}
