namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.EstudiosAmbientales.Requests;

public sealed record DeleteEstudioAmbientalRequest
(
    long Id,
    string UsuarioEliminacion
);