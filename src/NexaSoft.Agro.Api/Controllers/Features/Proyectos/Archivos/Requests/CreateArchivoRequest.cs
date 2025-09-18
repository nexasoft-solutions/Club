namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Request;

public sealed record CreateArchivoRequest(
    IFormFile Archivo,
    string? DescripcionArchivo,
    int TipoArchivoId,
    long? SubCapituloId,
    long? EstructuraId,
    string? NombreCorto,
    string? UsuarioCreacion
);
