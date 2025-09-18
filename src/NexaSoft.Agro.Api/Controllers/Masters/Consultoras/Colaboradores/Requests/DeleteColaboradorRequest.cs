namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Colaboradores.Requests;

public record class DeleteColaboradorRequest
(
    long Id,
    string UsuarioEliminacion
);
