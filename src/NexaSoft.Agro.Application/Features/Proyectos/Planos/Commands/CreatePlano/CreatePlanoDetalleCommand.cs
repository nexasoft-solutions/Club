using NetTopologySuite.Geometries;

namespace NexaSoft.Agro.Application.Features.Proyectos.Planos.Commands.CreatePlano;

public sealed record CreatePlanoDetalleCommand
(
    Guid PlanoId,
    string? Descripcion,
    Geometry Coordenadas
);