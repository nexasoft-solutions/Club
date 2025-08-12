namespace NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

public sealed record CapituloResponse(
    Guid Id,
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    string? Proyecto,
    Guid EstudioAmbientalId,
    DateTime FechaCreacion
);
