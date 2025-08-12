namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Capitulos.Request;

public sealed record UpdateCapituloRequest(
   Guid Id,
    string? NombreCapitulo,
    string? DescripcionCapitulo,
    Guid EstudioAmbientalId
);
