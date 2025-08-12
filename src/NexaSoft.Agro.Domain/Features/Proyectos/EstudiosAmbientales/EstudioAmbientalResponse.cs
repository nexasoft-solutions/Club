namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

public sealed record EstudioAmbientalResponse(
    Guid Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    string? EstadoEstudioAmbiental,
    string? RazonSocial,
    Guid EmpresaId,
    string? CodigoEstudio,
    DateTime FechaCreacion
);
