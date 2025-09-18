using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.Cumplimientos.Commands.CreateCumplimiento;

public class CreateCumplimientoCommandValidator : AbstractValidator<CreateCumplimientoCommand>
{
    public CreateCumplimientoCommandValidator()
    {
        // Validación personalizada para FechaCumplimiento de tipo DateOnly
        // Validación personalizada para RegistradoaTiempo de tipo bool
        RuleFor(x => x.Observaciones)
            .NotEmpty().WithMessage("El campo Observaciones no puede estar vacío.");
        // Validación personalizada para EventoRegulatorio de tipo EventoRegulatorio
    }
}
