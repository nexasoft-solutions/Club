using NetTopologySuite.Geometries;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos;

public class PlanoDetalle : Entity
{
    public Guid PlanoId { get; set; }
    public string? Descripcion { get;  set; }
    public Geometry Coordenadas { get; set; }   
    public Plano Plano { get; set; } = null!;

    public PlanoDetalle(
        Guid id,
        Guid planoId,
        string? descripcion,
        Geometry coordenadas,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        PlanoId = planoId;
        Descripcion = descripcion;
        Coordenadas = coordenadas;
        FechaCreacion = fechaCreacion;
    }

    public static PlanoDetalle Create
    (
        Guid planoId,
        string? descripcion,
        Geometry coordenadas,
        DateTime fechaCreacion
    )
    {
        return new PlanoDetalle(
            Guid.NewGuid(),
            planoId,
            descripcion,
            coordenadas,
            fechaCreacion
        );
    }
}
