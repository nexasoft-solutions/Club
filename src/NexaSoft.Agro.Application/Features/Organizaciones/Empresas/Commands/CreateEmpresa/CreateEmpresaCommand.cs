using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.CreateEmpresa;

public sealed record CreateEmpresaCommand(
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
) : ICommand<Guid>;
