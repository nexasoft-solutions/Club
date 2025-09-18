namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos.Request;

public sealed record CreateCapituloRequest(
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    long EstudioAmbientalId,
    string? UsuarioCreacion
);
