using FluentValidation;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.CreateEstudioAmbiental;

public class CreateEstudioAmbientalCommandValidator : AbstractValidator<CreateEstudioAmbientalCommand>
{
    public CreateEstudioAmbientalCommandValidator()
    {
        RuleFor(x => x.Proyecto)
            .NotEmpty().WithMessage("El campo Proyecto no puede estar vacío.");
        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("El campo Detalles no puede estar vacío.");
        // Validación personalizada para Empresa de tipo Empresa
    }
}
