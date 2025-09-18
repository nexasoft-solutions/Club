namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales.Request;

public sealed record CreateEstudioAmbientalRequest(
    string? Proyecto,
    DateOnly FechaInicio,
    DateOnly FechaFin,
    string? Detalles,
    long EmpresaId,
    string? UsuarioCreacion
);
