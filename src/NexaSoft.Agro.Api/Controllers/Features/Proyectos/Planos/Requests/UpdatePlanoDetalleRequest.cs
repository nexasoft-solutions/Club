using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Requests;

public sealed record UpdatePlanoDetalleRequest
(
    long Id,
    long planoId,
    string? Descripcion,
    Geometry Coordenadas,
    List<UpdatePlanoDetalleRequest> Detalles
);