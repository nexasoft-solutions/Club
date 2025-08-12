using NexaSoft.Agro.Domain.Abstractions;
using NexaSoft.Agro.Domain.Masters.Ubigeos.Events;
using static NexaSoft.Agro.Domain.Shareds.Enums;

namespace NexaSoft.Agro.Domain.Masters.Ubigeos;

public class Ubigeo : Entity
{
    public string? Descripcion { get; private set; }
    public int Nivel { get; private set; }
    public Guid? PadreId { get; private set; }
    public Ubigeo? Padre { get; private set; }
    public int EstadoUbigeo { get; private set; }

    private Ubigeo() { }

    private Ubigeo(
        Guid id, 
        string? descripcion, 
        int nivel, 
        Guid? padreId,
        int estadoUbigeo, 
        DateTime fechaCreacion
    ) : base(id, fechaCreacion)
    {
        Descripcion = descripcion;
        Nivel = nivel;
        PadreId = padreId;
        EstadoUbigeo = estadoUbigeo;
        FechaCreacion = fechaCreacion;
    }

    public static Ubigeo Create(
        string? descripcion, 
        int nivel, 
        Guid? padreId,
        int estadoUbigeo, 
        DateTime fechaCreacion
    )
    {
        var entity = new Ubigeo(
            Guid.NewGuid(),
            descripcion,
            nivel,
            padreId,
            estadoUbigeo,
            fechaCreacion
        );
        entity.RaiseDomainEvent(new UbigeoCreateDomainEvent(entity.Id));
        return entity;
    }

    public Result Update(
        Guid Id,
        string? descripcion,
        int nivel,
        Guid? padreId,
        DateTime utcNow
    )
    {
        Descripcion = descripcion;
        Nivel = nivel;
        PadreId = padreId;        
        FechaModificacion = utcNow;

        RaiseDomainEvent(new UbigeoUpdateDomainEvent(this.Id));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        EstadoUbigeo = (int)EstadosEnum.Eliminado;
        FechaEliminacion = utcNow;
        return Result.Success();
    }
}
