namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales.Request;

public sealed record UpdateEstudioAmbientalRequest(
    long Id,
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    long EmpresaId,
    string? UsuarioModificacion
);
