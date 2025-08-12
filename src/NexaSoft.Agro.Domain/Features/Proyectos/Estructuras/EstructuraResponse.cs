namespace NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

public sealed record EstructuraResponse(
    Guid Id,
    string? TipoEstructura,
    string? NombreEstructura,
    string? DescripcionEstructura,
    string? NombreSubCapitulo,
    Guid? PadreEstructuraId,
    Guid SubCapituloId,
    int TipoEstructuraId,
    DateTime FechaCreacion
);
