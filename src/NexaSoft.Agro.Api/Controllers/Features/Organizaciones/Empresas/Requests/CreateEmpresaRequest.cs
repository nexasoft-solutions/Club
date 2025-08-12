namespace NexaSoft.Agro.Api.Controllers.Features.Organizaciones.Empresas.Request;

public sealed record CreateEmpresaRequest(
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
);
