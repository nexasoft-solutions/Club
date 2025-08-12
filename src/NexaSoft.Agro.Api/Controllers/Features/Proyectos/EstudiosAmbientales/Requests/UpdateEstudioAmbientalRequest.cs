namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales.Request;

public sealed record UpdateEstudioAmbientalRequest(
    Guid Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    Guid EmpresaId
);
