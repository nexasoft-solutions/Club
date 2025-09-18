namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras.Request;

public sealed record UpdateEstructuraRequest(
    long Id,
    int TipoEstructuraId,
    string? NombreEstructura,
    string? DescripcionEstructura,
    long? PadreEstructuraId,
    long SubCapituloId,
    string? UsuarioModificacion
);
