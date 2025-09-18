namespace NexaSoft.Agro.Api.Controllers.Masters.Consultoras.Requests;

public sealed record DeleteConsultoraRequest
(
    long Id,
    string UsuarioEliminacion
);
