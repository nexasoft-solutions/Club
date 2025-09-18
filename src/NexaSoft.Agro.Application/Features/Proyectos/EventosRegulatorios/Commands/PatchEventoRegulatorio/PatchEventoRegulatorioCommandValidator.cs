using FluentValidation;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.PatchEventoRegulatorio;

public class PatchEventoRegulatorioCommandValidator : AbstractValidator<PatchEventoRegulatorioCommand>
{
    public PatchEventoRegulatorioCommandValidator()
    {
        RuleFor(x => x.FechaVencimiento)
           .NotNull()
           .When(x => x.NuevoEstado == (int)EstadosEventosEnum.Reprogramado)
           .WithMessage("Debe ingresar una Fecha de Vencimiento si el tipo de evento es Reprogramación.");
    }
}
