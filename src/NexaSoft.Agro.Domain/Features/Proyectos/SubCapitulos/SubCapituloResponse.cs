namespace NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

public sealed record SubCapituloResponse(
    Guid Id,
    string? NombreSubCapitulo,
    string? DescripcionSubCapitulo,
    string NombreCapitulo,
    Guid CapituloId,
    DateTime FechaCreacion
);
