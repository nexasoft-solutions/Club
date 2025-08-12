using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.UpdateEstudioAmbiental;

public sealed record UpdateEstudioAmbientalCommand(
    Guid Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    Guid EmpresaId
) : ICommand<bool>;
