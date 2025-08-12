using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

public sealed record ArchivoResponse(
    Guid Id,
    string? NombreArchivo,
    string? DescripcionArchivo,
    string? RutaArchivo,
    DateOnly FechaCarga,
    string? TipoArchivo,
    Guid? SubCapituloId,
    Guid? EstructuraId,
    DateTime FechaCreacion,
    int TipoArchivoId
    //PlanoResponse? Plano = null
);
