namespace NexaSoft.Agro.Domain.Features.Organizaciones;

public sealed record OrganizacionResponse(
    long Id,
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    string? RucOrganizacion,
    string? Observaciones,
    string? Sector,
    int SectorId,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
