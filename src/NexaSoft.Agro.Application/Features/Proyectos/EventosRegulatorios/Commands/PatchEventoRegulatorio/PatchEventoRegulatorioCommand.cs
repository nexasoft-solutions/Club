using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EventosRegulatorios.Commands.PatchEventoRegulatorio;

public sealed record PatchEventoRegulatorioCommand
(
    long Id,
    int NuevoEstado,
    string Observaciones,
    string UsuarioModificacion,
    DateOnly? FechaVencimiento = null
):ICommand<bool>;