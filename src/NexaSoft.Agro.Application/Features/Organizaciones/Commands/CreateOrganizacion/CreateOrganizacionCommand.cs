using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Commands.CreateOrganizacion;

public sealed record CreateOrganizacionCommand(
    string? NombreOrganizacion,
    string? ContactoOrganizacion,
    string? TelefonoContacto,
    int SectorId
) : ICommand<Guid>;
