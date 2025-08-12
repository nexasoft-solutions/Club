using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.UpdateOrganizacion;

public sealed record UpdateOrganizacionCommand(
    Guid Id,
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    int SectorId
) : ICommand<bool>;
