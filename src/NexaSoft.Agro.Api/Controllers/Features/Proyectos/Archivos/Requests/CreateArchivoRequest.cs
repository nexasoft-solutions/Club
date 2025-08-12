namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Request;

public sealed record CreateArchivoRequest(
    IFormFile Archivo,
    string? DescripcionArchivo,
    int TipoArchivo,
    Guid? SubCapituloId,
    Guid? EstructuraId
);
