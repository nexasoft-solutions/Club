namespace NexaSoft.Agro.Domain.Features.Organizaciones;

public sealed record OrganizacionResponse(
    Guid Id,
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    string? Sector,
    DateTime FechaCreacion
);
