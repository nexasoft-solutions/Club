using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.UpdateOrganizacion;

public sealed record UpdateOrganizacionCommand(
    long Id,
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    string? RucOrganizacion,
    string? Observaciones,
    int SectorId,
    string? UsuarioModificacion
) : ICommand<bool>;
