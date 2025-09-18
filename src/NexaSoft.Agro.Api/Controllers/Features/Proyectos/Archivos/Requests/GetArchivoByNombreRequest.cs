namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Archivos.Requests;

public sealed record GetArchivoByNombreRequest
(
    long EstudioAmbientalId,
    string Filtro
);
