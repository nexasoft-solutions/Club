namespace NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

public sealed record EmpresaResponse(
    long Id,
    string? RazonSocial,
    string? RucEmpresa,
    string? ContactoEmpresa,
    string? TelefonoContactoEmpresa,
    string? DepartamentoEmpresa,
    string? ProvinciaEmpresa,
    string? DistritoEmpresa,
    string? Direccion,
    double LatitudEmpresa,
    double LongitudEmpresa,
    string? Organizacion,
    long DepartamentoEmpresaId,
    long ProvinciaEmpresaId,
    long DistritoEmpresaId,
    long OrganizacionId,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
