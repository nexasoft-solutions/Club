using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.UpdateEmpresa;

public sealed record UpdateEmpresaCommand(
    Guid Id,
    string? RazonSocial,
    string? RucEmpresa,
    string? ContactoEmpresa,
    string? TelefonoContactoEmpresa,
    Guid DepartamentoEmpresaId,
    Guid ProvinciaEmpresaId,
    Guid DistritoEmpresaId,
    string? Direccion,
    double LatitudEmpresa,
    double LongitudEmpresa,
    Guid OrganizacionId
) : ICommand<bool>;
