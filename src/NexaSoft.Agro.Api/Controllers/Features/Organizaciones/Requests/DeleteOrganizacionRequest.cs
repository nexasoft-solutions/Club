namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Requests;

public sealed record DeleteOrganizacionRequest
(
    long Id,
    string UsuarioEliminacion
);