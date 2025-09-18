namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.SubCapitulos.Requests;

public sealed record DeleteSubCapituloRequest
(
    long Id,
    string UsuarioEliminacion
);