namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Request;

public sealed record CreateOrganizacionRequest(
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    string? RucOrganizacion,
    string? Observaciones,
    int SectorId,
    string? UsuarioCreacion
);
