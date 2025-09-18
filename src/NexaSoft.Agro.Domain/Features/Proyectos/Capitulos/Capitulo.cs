using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.EstudiosAmbientales;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Capitulos;

public class Capitulo : Entity
{
    public string? NombreCapitulo { get; private set; }
    public string? DescripcionCapitulo { get; private set; }
    public int EstadoCapitulo { get; private set; }
    public long EstudioAmbientalId { get; private set; }
    public EstudioAmbiental? EstudioAmbiental { get; private set; }

    //public ICollection<SubCapitulo> SubCapitulos { get; private set; } = new List<SubCapitulo>();

    private Capitulo() { }

    private Capitulo(
        string? nombreCapitulo,
        string? descripcionCapitulo,
        int estadoCapitulo,
        long estudioAmbientalId,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)

    {
        NombreCapitulo = nombreCapitulo;
        DescripcionCapitulo = descripcionCapitulo;
        EstadoCapitulo = estadoCapitulo;
        EstudioAmbientalId = estudioAmbientalId;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Capitulo Create(
        string? nombreCapitulo,
        string? descripcionCapitulo,
        int estadoCapitulo,
        long estudioAmbientalId,
        DateTime fechaCreacion,        
        string? usuarioCreacion
    )
    {
        var entity = new Capitulo(
            nombreCapitulo,
            descripcionCapitulo,
            estadoCapitulo,
            estudioAmbientalId,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new CapituloCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        string? nombreCapitulo,
        string? descripcionCapitulo,
        long estudioAmbientalId,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        NombreCapitulo = nombreCapitulo;
        DescripcionCapitulo = descripcionCapitulo;
        EstudioAmbientalId = estudioAmbientalId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new CapituloUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow, string usuarioEliminacion)
    {
        EstadoCapitulo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
