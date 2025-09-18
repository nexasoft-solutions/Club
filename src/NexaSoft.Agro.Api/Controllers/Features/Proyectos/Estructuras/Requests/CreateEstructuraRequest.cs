namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Estructuras.Request;

public sealed record CreateEstructuraRequest(
    int TipoEstructuraId,
    string? NombreEstructura,
    string? DescripcionEstructura,
    long? PadreEstructuraId,
    long SubCapituloId,
    string? UsuarioCreacion
);
