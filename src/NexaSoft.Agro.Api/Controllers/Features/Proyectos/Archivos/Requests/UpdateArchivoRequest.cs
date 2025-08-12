namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Request;

public sealed record UpdateArchivoRequest(
    Guid Id,
    string? DescripcionArchivo 
);
