namespace NexaSoft.Agro.Api.Controllers.Masters.Ubigeos.Requests;

public sealed record DeleteUbigeoRequest
(
    long Id,
    string UsuarioEliminacion
);