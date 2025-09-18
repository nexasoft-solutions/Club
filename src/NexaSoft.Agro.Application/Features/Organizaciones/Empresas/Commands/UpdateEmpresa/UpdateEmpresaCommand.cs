using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.UpdateEmpresa;

public sealed record UpdateEmpresaCommand(
    long Id,
    string? RazonSocial,
    string? RucEmpresa,
    string? ContactoEmpresa,
    string? TelefonoContactoEmpresa,
    long DepartamentoEmpresaId,
    long ProvinciaEmpresaId,
    long DistritoEmpresaId,
    string? Direccion,
    double LatitudEmpresa,
    double LongitudEmpresa,
    long OrganizacionId,
    string? UsuarioModificacion
) : ICommand<bool>;
