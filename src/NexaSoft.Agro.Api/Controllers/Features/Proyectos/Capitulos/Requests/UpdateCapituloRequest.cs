namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos.Request;

public sealed record UpdateCapituloRequest(
   long Id,
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    long EstudioAmbientalId,
    string? UsuarioModificacion
);
