namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Empresas.Requests;

public sealed record DeleteEmpresaRequest
(
    long Id,
    string UsuarioEliminacion
);
