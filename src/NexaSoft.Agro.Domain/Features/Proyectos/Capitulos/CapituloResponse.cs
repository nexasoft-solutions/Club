namespace NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

public sealed record CapituloResponse(
    long Id,
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    string? Proyecto,
    long EstudioAmbientalId,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
