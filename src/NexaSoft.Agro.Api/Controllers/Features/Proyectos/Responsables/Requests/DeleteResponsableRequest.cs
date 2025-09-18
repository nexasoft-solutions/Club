namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Responsables.Requests;

public sealed record DeleteResponsableRequest
(
    long Id,
    string UsuarioEliminacion
);
