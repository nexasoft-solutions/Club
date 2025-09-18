namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos.Requests;

public record class DeleteCapituloRequest
(
    long Id,
    string UsuarioEliminacion
);
