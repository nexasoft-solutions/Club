namespace NexaSoft.Agro.Domain.Features.Organizaciones.Empresas;

public sealed record EmpresaResponse(
    Guid Id,
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
    Guid DepartamentoEmpresaId,
    Guid ProvinciaEmpresaId,
    Guid DistritoEmpresaId,
    Guid OrganizacionId,
    DateTime FechaCreacion
);
