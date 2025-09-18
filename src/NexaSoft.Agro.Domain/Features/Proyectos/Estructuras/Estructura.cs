using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Features.Proyectos.Estructuras.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;
using NexaSoft.Agro.Domain.Features.Proyectos.SubCapitulos;

namespace NexaSoft.Agro.Domain.Features.Proyectos.Estructuras;

public class Estructura : Entity
{
    public int TipoEstructuraId { get; private set; }
    public string? NombreEstructura { get; private set; }
    public string? DescripcionEstructura { get; private set; }
    public long? PadreEstructuraId { get; private set; }
    public long SubCapituloId { get; private set; }
    public int EstadoEstructura { get; private set; }
    public SubCapitulo? SubCapitulo { get; private set; }

    public Estructura? Padre { get; private set; }
    //public ICollection<Archivo> Archivos { get; private set; } = new List<Archivo>();

    public ICollection<Estructura> Hijos { get; private set; } = new List<Estructura>();

    private Estructura() { }

    private Estructura(
        int tipoEstructuraId,
        string? nombreEstructura,
        string? descripcionEstructura,
        long? padreEstructuraId,
        long subCapituloId,
        int estadoEstructura,
        DateTime fechaCreacion,
        string? usuarioCreacion,
        string? usuarioModificacion = null,
        string? usuarioEliminacion = null
    ) : base(fechaCreacion, usuarioCreacion, usuarioModificacion, usuarioEliminacion)
    {
        TipoEstructuraId = tipoEstructuraId;
        NombreEstructura = nombreEstructura;
        DescripcionEstructura = descripcionEstructura;
        PadreEstructuraId = padreEstructuraId;
        SubCapituloId = subCapituloId;
        EstadoEstructura = estadoEstructura;
        FechaCreacion = fechaCreacion;
        UsuarioCreacion = usuarioCreacion;
        UsuarioModificacion = usuarioModificacion;
        UsuarioEliminacion = usuarioEliminacion;
    }

    public static Estructura Create(
        int tipoEstructuraId,
        string? nombreEstructura,
        string? descripcionEstructura,
        long? padreEstructuraId,
        long subCapituloId,
        int estadoEstructura,
        DateTime fechaCreacion,
        string? usuarioCreacion
    )
    {
        var entity = new Estructura(
            tipoEstructuraId,
            nombreEstructura,
            descripcionEstructura,
            padreEstructuraId,
            subCapituloId,
            estadoEstructura,
            fechaCreacion,
            usuarioCreacion
        );
        //entity.RaiseDomainEvent(new EstructuraCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        long Id,
        int tipoEstructuraId,
        string? nombreEstructura,
        string? descripcionEstructura,
        long? padreEstructuraId,
        long subCapituloId,
        DateTime utcNow,
        string? usuarioModificacion
    )
    {
        TipoEstructuraId = tipoEstructuraId;
        NombreEstructura = nombreEstructura;
        DescripcionEstructura = descripcionEstructura;
        PadreEstructuraId = padreEstructuraId;
        SubCapituloId = subCapituloId;
        FechaModificacion = utcNow;
        UsuarioModificacion = usuarioModificacion;

        //RaiseDomainEvent(new EstructuraUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow,string usuarioEliminacion)
    {
        EstadoEstructura = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        UsuarioEliminacion = usuarioEliminacion;
        return Result.Success();
    }
}
