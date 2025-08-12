namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Request;

public sealed record UpdateOrganizacionRequest(
   Guid Id,
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    int SectorId
);
