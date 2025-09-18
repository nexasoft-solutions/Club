using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.UpdateEstudioAmbiental;

public sealed record UpdateEstudioAmbientalCommand(
    long Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    long EmpresaId,
    string? UsuarioModificacion
) : ICommand<bool>;
