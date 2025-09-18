namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Requests;

public sealed record DeleteArchivoRequest
(
    long Id,
    string? UsuarioEliminacion
);