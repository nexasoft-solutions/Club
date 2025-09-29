using FluentValidation;

namespace NexaSoft.Club.Application.Masters.Constantes.Commands.CreateConstante;

public class CreateConstanteCommandValidator : AbstractValidator<CreateConstanteCommand>
{
    public CreateConstanteCommandValidator()
    {
        RuleFor(x => x.TipoConstante)
            .NotEmpty().WithMessage("El campo TipoConstante no puede estar vacío.");
        RuleFor(x => x.Valor)
            .NotEmpty().WithMessage("El campo Valor no puede estar vacío.");
    }
}
