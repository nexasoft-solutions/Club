namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Request;

public sealed record CreateOrganizacionRequest(
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    int SectorId
);
