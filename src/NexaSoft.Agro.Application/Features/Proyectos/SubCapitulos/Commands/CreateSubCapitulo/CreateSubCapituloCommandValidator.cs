using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.SubCapitulos.Commands.CreateSubCapitulo;

public class CreateSubCapituloCommandValidator : AbstractValidator<CreateSubCapituloCommand>
{
    public CreateSubCapituloCommandValidator()
    {
        RuleFor(x => x.NombreSubCapitulo)
            .NotEmpty().WithMessage("El campo NombreSubCapitulo no puede estar vacío.");
        RuleFor(x => x.DescripcionSubCapitulo)
            .NotEmpty().WithMessage("El campo DescripcionSubCapitulo no puede estar vacío.");
    }
}
