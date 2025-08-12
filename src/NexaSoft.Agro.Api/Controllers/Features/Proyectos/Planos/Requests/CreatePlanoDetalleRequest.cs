using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Api.Controllers.Features.Proyectos.Planos.Requests;

public sealed record CreatePlanoDetalleRequest
(
    string? Descripcion,
    Geometry Coordenadas
);
