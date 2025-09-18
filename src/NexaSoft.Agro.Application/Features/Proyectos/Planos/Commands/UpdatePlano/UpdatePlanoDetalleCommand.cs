using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.UpdatePlano;

public sealed record UpdatePlanoDetalleCommand
(
    long Id,
    long planoId,
    string? Descripcion,
    Geometry Coordenadas
);
