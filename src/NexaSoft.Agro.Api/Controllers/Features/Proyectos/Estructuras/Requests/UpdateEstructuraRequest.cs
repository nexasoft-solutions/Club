namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras.Request;

public sealed record UpdateEstructuraRequest(
    Guid Id,
    int TipoEstructuraId,
    string? NombreEstructura,
    string? DescripcionEstructura,
    Guid? PadreEstructuraId,
    Guid SubCapituloId
);
