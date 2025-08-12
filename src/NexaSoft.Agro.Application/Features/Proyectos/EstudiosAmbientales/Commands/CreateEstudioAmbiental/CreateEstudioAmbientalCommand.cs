using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Proyectos.EstudiosAmbientales.Commands.CreateEstudioAmbiental;

public sealed record CreateEstudioAmbientalCommand(
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    Guid EmpresaId
) : ICommand<Guid>;
