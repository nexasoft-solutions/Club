namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Empresas.Request;

public sealed record UpdateEmpresaRequest(
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
);
