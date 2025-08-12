using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Requests;

public sealed record UpdatePlanoDetalleRequest
(
    Guid Id,
    Guid planoId,
    string? Descripcion,
    Geometry Coordenadas,
    List<UpdatePlanoDetalleRequest> Detalles
);