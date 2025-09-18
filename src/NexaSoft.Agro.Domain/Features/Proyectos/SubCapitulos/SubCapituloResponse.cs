namespace NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

public sealed record SubCapituloResponse(
    long Id,
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    string NombreCapitulo,
    long CapituloId,
    DateTime FechaCreacion,
    DateTime? FechaModificacion,
    string? UsuarioCreacion,
    string? UsuarioModificacion
);
