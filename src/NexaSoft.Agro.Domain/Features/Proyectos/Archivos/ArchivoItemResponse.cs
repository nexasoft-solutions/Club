namespace NexaSoft.Agro.Domain.Features.Proyectos.Archivos;

public sealed record ArchivoItemResponse
(
    long Id,
    long? CapituloId,
    long? SubCapituloId,
    long? SubCapDirecto,
    long? EstructuraId,
    string? NombreCorto,
    string? Estructura
);
