namespace NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

public sealed record EstructuraResponse(
    long Id,
    string? TipoEstructura,
    string? NombreEstructura,
    string? DescripcionEstructura,
    string? NombreSubCapitulo,
    long? PadreEstructuraId,
    long SubCapituloId,
    int TipoEstructuraId,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
