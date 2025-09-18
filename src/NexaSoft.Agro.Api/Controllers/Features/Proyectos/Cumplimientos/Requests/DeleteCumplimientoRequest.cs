namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Cumplimientos.Requests;

public sealed record DeleteCumplimientoRequest
(
    long Id,
    string UsuarioEliminacion
);
