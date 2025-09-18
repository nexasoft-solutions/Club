namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras.Requests;

public sealed record DeleteEstructuraRequest
(
    long Id,
    string UsuarioEliminacion
);