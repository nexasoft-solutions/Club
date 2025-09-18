using NexaSoft.Agro.Domain.Features.Proyectos.Planos;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

public sealed record ArchivoResponse(
    long Id,
    string? NombreArchivo,
    string? DescripcionArchivo,
    string? RutaArchivo,
    DateOnly FechaCarga,
    string? TipoArchivo,
    long? SubCapituloId,
    long? EstructuraId,
    string? NombreCorto,
    DateTime FechaCreacion,
    int TipoArchivoId,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
    //PlanoResponse? Plano = null
);
