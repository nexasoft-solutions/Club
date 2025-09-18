namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Request;

public sealed record UpdateArchivoRequest(
    long Id,
    string? DescripcionArchivo,
    string? NombreCorto,
    string? UsuarioModificacion
);
