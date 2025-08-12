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
    public Guid? PadreEstructuraId { get; private set; }
    public Guid SubCapituloId { get; private set; }
    public int EstadoEstructura { get; private set; }
    public SubCapitulo? SubCapitulo { get; private set; }

    public Estructura? Padre { get; private set; }
    //public ICollection<Archivo> Archivos { get; private set; } = new List<Archivo>();

    public ICollection<Estructura> Hijos { get; private set; } = new List<Estructura>();

    private Estructura() { }

    private Estructura(
        Guid id,
        int tipoEstructuraId,
        string? nombreEstructura,
        string? descripcionEstructura,
        Guid? padreEstructuraId,
        Guid subCapituloId,
        int estadoEstructura,
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        TipoEstructuraId = tipoEstructuraId;
        NombreEstructura = nombreEstructura;
        DescripcionEstructura = descripcionEstructura;
        PadreEstructuraId = padreEstructuraId;
        SubCapituloId = subCapituloId;
        EstadoEstructura = estadoEstructura;
        FechaCreacion = fechaCreacion;
    }

    public static Estructura Create(
        int tipoEstructuraId,
        string? nombreEstructura,
        string? descripcionEstructura,
        Guid? padreEstructuraId,
        Guid subCapituloId,
        int estadoEstructura,
        DateTime fechaCreacion
    )
    {
        var entity = new Estructura(
            Guid.NewGuid(),
            tipoEstructuraId,
            nombreEstructura,
            descripcionEstructura,
            padreEstructuraId,
            subCapituloId,
            estadoEstructura,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new EstructuraCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        int tipoEstructuraId,
        string? nombreEstructura,
        string? descripcionEstructura,
        Guid? padreEstructuraId,
        Guid subCapituloId,
        DateTime utcNow
    )
    {
        TipoEstructuraId = tipoEstructuraId;
        NombreEstructura = nombreEstructura;
        DescripcionEstructura = descripcionEstructura;
        PadreEstructuraId = padreEstructuraId;
        SubCapituloId = subCapituloId;
        FechaModificacion = utcNow;

        RaiseDomainEvent(new EstructuraUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoEstructura = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
