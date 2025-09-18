namespace NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;

public sealed record EstudioAmbientalResponse(
    long Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    string? EstadoEstudioAmbiental,
    string? RazonSocial,
    long EmpresaId,
    string? CodigoEstudio,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
