using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.UpdatePlano;

public sealed record UpdatePlanoDetalleCommand
(
    Guid Id,
    Guid planoId,
    string? Descripcion,
    Geometry Coordenadas
);
