using NexaSoft.Agro.Application.Abstractions.Messaging;

namespace NexaSoft.Agro.Application.Features.Organizaciones.Empresas.Commands.CreateEmpresa;

public sealed record CreateEmpresaCommand(
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
    string? UsuarioCreacion
) : ICommand<long>;
