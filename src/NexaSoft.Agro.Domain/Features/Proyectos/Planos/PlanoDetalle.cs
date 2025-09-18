using NetTopologySuite.Geometries;
using NexaSoft.Agro.Domain.Abstractions;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Planos;

public class PlanoDetalle : Entity
{
    public long PlanoId { get; set; }
    public string? Descripcion { get;  set; }
    public Geometry Coordenadas { get; set; }   
    public Plano Plano { get; set; } = null!;

    public PlanoDetalle(
        long planoId,
        string? descripcion,
        Geometry coordenadas,
        DateTime fechaCreacion,
        string usuarioCreacion        
    ) : base(fechaCreacion, usuarioCreacion)
    {
        PlanoId = planoId;
        Descripcion = descripcion;
        Coordenadas = coordenadas;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;  
    }

    public static PlanoDetalle Create
    (
        long planoId,
        string? descripcion,
        Geometry coordenadas,
        DateTime fechaCreacion,
        string usuarioCreacion
    )
    {
        return new PlanoDetalle(
            planoId,
            descripcion,
            coordenadas,
            fechaCreacion,
            usuarioCreacion
        );
    }
}
