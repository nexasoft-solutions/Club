using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.AddResposablesEvento;

public class AddResponsablesEventoCommandValidator: AbstractValidator<AddResponsablesEventoCommand>
{
    public AddResponsablesEventoCommandValidator()
    {
        RuleFor(x => x.EventoRegulatorioId)
            .GreaterThan(0).WithMessage("El ID del evento es obligatorio.");

        RuleFor(x => x.ResponsablesIds)
            .NotEmpty().WithMessage("Debe proporcionar al menos un responsable.");

    }
}
