using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.CreateEventoRegulatorio;

public class CreateEventoRegulatorioCommandValidator : AbstractValidator<CreateEventoRegulatorioCommand>
{
    public CreateEventoRegulatorioCommandValidator()
    {
        RuleFor(x => x.NombreEvento)
            .NotEmpty().WithMessage("El campo NombreEvento no puede estar vacío.");
        // Validación personalizada para FechaExpedición de tipo DateOnly
        // Validación personalizada para FechaVencimiento de tipo DateOnly
        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("El campo Descripcion no puede estar vacío.");
        // Validación personalizada para Responsable de tipo Responsable
        // Validación personalizada para EstudioAmbiental de tipo EstudioAmbiental
    }
}
